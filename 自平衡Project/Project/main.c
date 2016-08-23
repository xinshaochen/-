#include "delay.h"
#include "led.h"
#include "wave.h"
#include "motor.h"
#include "pid.h"
#include "sensor.h"
#include "timer.h"
#include "Uart.h"
#include "UartProtocol.h"
#include "mpu6050.h"
#include "inv_mpu_dmp_motion_driver.h"
#include "inv_mpu.h"
#include "DetectionLogic.h"
#include "RC522.h"
#include "adc.h"
#include "wifi.h"
#include "encoder.h"
#include "servo.h"
#include "math.h"
#include "filter.h"
#define PI 3.14159265
#define FilterMode 1

float Angle_Y,Gyro_Y,Gyro_Turn;//倾斜角度与平衡角速度与转向角速度
float Acc_Z;//Z轴加速度

PIDStruct LPID,RPID;
s16 LNow,RNow;
s16 LTarget=0,RTarget=0;

float KP=50,EI=0,KD=0,EP=3;
u8 Stand=0;

//=====================================================================================================
//q30格式,long转float时的除数.
#define q30  1073741824.0f

u8 ReadDMP(short *gyro,short *accel,float *Pitch,float *Roll,float *Yaw)
{
	  unsigned long sensor_timestamp;
		unsigned char more;
		long quat[4];
		short sensors;
		float q0=1.0f,q1=0.0f,q2=0.0f,q3=0.0f;

				dmp_read_fifo(gyro, accel, quat, &sensor_timestamp, &sensors, &more);		
				if (sensors & INV_WXYZ_QUAT )
				{    
					 q0=quat[0] / q30;
					 q1=quat[1] / q30;
					 q2=quat[2] / q30;
					 q3=quat[3] / q30;
					 *Pitch = asin(-2 * q1 * q3 + 2 * q0* q2)* 57.3;
					 //*Roll  = atan2(2 * q2 * q3 + 2 * q0 * q1, -2 * q1 * q1 - 2 * q2* q2 + 1)* 57.3;	// roll
					 //*Yaw   = atan2(2*(q1*q2 + q0*q3),q0*q0+q1*q1-q2*q2-q3*q3) * 57.3;	//yaw
					return 1;
				}
				return 0;
}

//获取角度的算法 1：DMP  2：卡尔曼 3：互补滤波
void GetMPU(u8 way)
{
	float Accel_Y,Accel_X,Accel_Z,Gyro_Y,Gyro_Z;
	short gyro[3], accel[3];
	float pitch,roll,yaw;
	if(way==1)
	{
		if(ReadDMP(gyro,accel,&pitch,&roll,&yaw))
		{
			Angle_Y=pitch;
			Gyro_Y=gyro[1];	
		}
	}else
	{
		Gyro_Y=(IIC.ReadOneByte(devAddr,MPU_GYRO_YOUTH_REG)<<8)+IIC.ReadOneByte(devAddr,MPU_GYRO_YOUTL_REG);    //读取Y轴陀螺仪
		Gyro_Z=(IIC.ReadOneByte(devAddr,MPU_GYRO_ZOUTH_REG)<<8)+IIC.ReadOneByte(devAddr,MPU_GYRO_ZOUTL_REG);    //读取Z轴陀螺仪
		Accel_X=(IIC.ReadOneByte(devAddr,MPU_ACCEL_XOUTH_REG)<<8)+IIC.ReadOneByte(devAddr,MPU_ACCEL_XOUTL_REG); //读取X轴加速度计
		Accel_Z=(IIC.ReadOneByte(devAddr,MPU_ACCEL_ZOUTH_REG)<<8)+IIC.ReadOneByte(devAddr,MPU_ACCEL_ZOUTL_REG); //读取Z轴加速度计
		if(Gyro_Y>32768)  Gyro_Y-=65536;                       //数据类型转换  也可通过short强制类型转换
		if(Gyro_Z>32768)  Gyro_Z-=65536;                       //数据类型转换
		if(Accel_X>32768) Accel_X-=65536;                      //数据类型转换
		if(Accel_Z>32768) Accel_Z-=65536;                      //数据类型转换
		Gyro_Y=-Gyro_Y;                                  //更新平衡角速度
		Accel_Y=atan2(Accel_X,Accel_Z)*180/PI;                 //计算倾角	
		Gyro_Y=Gyro_Y/16.4;                                    //陀螺仪量程转换	
		if(way==2)		  	Kalman_Filter(Accel_Y,-Gyro_Y);//卡尔曼滤波	
		else if(way==3)   Yijielvbo(Accel_Y,-Gyro_Y);    //互补滤波
		Angle_Y=angle;                                   //更新平衡倾角
		Gyro_Turn=Gyro_Z;                                      //更新转向角速度
		Acc_Z=Accel_Z;                                //===更新Z轴加速度计	
	}
	
}


#define ZHONGZHI 0
/**************************************************************************
函数功能：直立PD控制
入口参数：角度、角速度
返回  值：直立控制PWM
**************************************************************************/
int balance(float Angle,float Gyro)
{  
//float kp=-3,kd=-0.1;
   float Bias;
	 int balance;
	 Bias=Angle-ZHONGZHI;       //===求出平衡的角度中值 和机械相关
	 balance=-KP*Bias+Gyro*-KD;   //===计算平衡控制的电机PWM  PD控制   kp是P系数 kd是D系数 
	 return balance;
}
/**************************************************************************
函数功能：速度PI控制 修改前进后退速度，请修Target_Velocity，比如，改成60就比较慢了
入口参数：左轮编码器、右轮编码器
返回  值：速度控制PWM
**************************************************************************/
int velocity()
{  
    static float Velocity,Encoder_Least,Encoder,Movement; 
	  static float Encoder_Integral;
	  //float kp=5,ki=kp/200;
	  //=============速度PI控制器=======================//	
		Encoder_Least =(LNow+RNow)-0;                    //===获取最新速度偏差==测量速度（左右编码器之和）-目标速度（此处为零） 
		Encoder *= 0.7;		                                                //===一阶低通滤波器       
		Encoder += Encoder_Least*0.3;	                                    //===一阶低通滤波器    
		Encoder_Integral +=Encoder;                                       //===积分出位移 积分时间：10ms
		Encoder_Integral=Encoder_Integral-Movement;                       //===接收遥控器数据，控制前进后退
		if(Encoder_Integral>7000)  	Encoder_Integral=7000;             //===积分限幅
		if(Encoder_Integral<-7000) 	Encoder_Integral=-7000;            //===积分限幅	
		Velocity=Encoder*EP+Encoder_Integral*EI;                          //===速度控制	
		return Velocity;
}

void control()
{
	
	int Balance_Pwm=0,Velocity_Pwm=0;
	s16 Lmotor,Rmotor;
	LNow=Encoder.LEncoder();
	RNow=Encoder.REncoder();
	
	//GetMPU(FilterMode);
	
	Balance_Pwm=balance(Angle_Y,Gyro_Y);
	//Velocity_Pwm=velocity();
	Lmotor=Balance_Pwm+Velocity_Pwm;
	Rmotor=Balance_Pwm+Velocity_Pwm;
	
	
	if(Stand)
	{
		if(Angle_Y<=40&&Angle_Y>=-40)
		{
			Motor.setSpeed(Lmotor,Rmotor);
		}
		else
		{
			Motor.setSpeed(0,0);
		}
	}
	//Lmotor = PID.PositionPID(&LPID,LNow,LTarget);
	//Rmotor = PID.PositionPID(&RPID,RNow,RTarget);
}
void GetMPUTick()
{
	GetMPU(FilterMode);
}


u8 butbit;
void tick()
{
	butbit=!butbit;
	if(butbit)
	{
		LTarget=0;
		RTarget=0;
	}
	else 
	{
		LTarget=0;
		RTarget=0;
	}
}






void GetDataEvent(UartEvent e)
{
	u8 i; 
	u8 id;
	u8 cmd;
	id=e->ReadByte();
	cmd=e->ReadByte();
	e->WriteByte(id);
	e->WriteByte(cmd);
	switch(cmd)
	{
		case All:
			
			
			e->WriteDWord(Angle_Y);
			e->WriteDWord(Gyro_Y);
			break;
		
	}
	e->SendAckPacket();
}
void SetDataEvent(UartEvent e)
{
	u8 id;
	u8 cmd;
	s16 ls,rs;
	
	id=e->ReadByte();
	cmd=e->ReadByte();
	e->WriteByte(id);
	e->WriteByte(cmd);
	e->SendAckPacket();
	switch(cmd)
	{
		case Pid:
				KP=e->ReadWord();
				KD=e->ReadWord();
			KD=KD/10;
				EP=e->ReadWord();
				EI=e->ReadWord();
			EI=EI/10;
			break;
		case SPEED:
			ls=e->ReadWord();
			rs=e->ReadWord();
			Motor.setSpeed(ls,rs);
			break;
		case STAND:
			Stand=e->ReadByte();
			break;
	}
}
int main(void)
{
	Stm32_Clock_Init(9);
	JTAG_Set(1);
	Motor.Init();
	Encoder.Init();
	PID.Init(&LPID,5,0,50);
	PID.Init(&RPID,5,0,50);
	Timer.Init(72);
	delay_init(72);
	if(!MPU.Init())
	{
		while(mpu_dmp_init())
		{
			delay_ms(500);
		}
	}
	
	
	UART.Init(72,115200,OnRecvData);
	UART.SendByte(0);
	
	Timer.Start(5,GetMPUTick);
	Timer.Start(10,control);
	Timer.Start(1500,tick);
	
	UartProtocol.Init(buffdata);
	UartProtocol.AutoAck(ENABLE);
//===========================================================
//wifi服务
	UartProtocol.RegisterCmd(Alive,wifi.uart.AliveEvent);
	UartProtocol.RegisterCmd(DeErr,wifi.uart.DeErrEvent);
	UartProtocol.RegisterCmd(sendMeIP,wifi.uart.SendMeIPEvent);
	Timer.Start(5,UartProtocol.Check);
	Timer.Start(100,wifi.Task.sendServing);
//===========================================================
	UartProtocol.RegisterCmd(GetData,GetDataEvent);
	UartProtocol.RegisterCmd(SetData,SetDataEvent);
	
	while(1)
	{
		Timer.Run();
	}
	

	
	
}
