#include"PID.h"

static void Init(PIDStruct *pid,float P,float I,float D)
{
	pid->Bias=pid->Integral_Bias=pid->Last_Bias=pid->results=0;
	pid->P = P;
	pid->I = I;
	pid->D = D;
}

/**************************************************************************
函数功能：位置式PID控制器
入口参数：编码器测量位置信息，目标位置
返回  值：电机PWM
根据位置式离散PID公式 
pwm=Kp*e(k)+Ki*∑e(k)+Kd[e（k）-e(k-1)]
e(k)代表本次偏差 
e(k-1)代表上一次的偏差  
∑e(k)代表e(k)以及之前的偏差的累积和;其中k为1,2,,k;
pwm代表输出
**************************************************************************/
static s16 PositionPID(PIDStruct *pid,s16 Now,s16 Target) 
{
	 pid->Bias=Now-Target;                                  //计算偏差
	 pid->Integral_Bias+=pid->Bias;	                                 //求出偏差的积分
	 pid->results=pid->P*pid->Bias+pid->I*pid->Integral_Bias+pid->D*(pid->Bias-pid->Last_Bias);//位置式PID控制器
	 pid->Last_Bias=pid->Bias;                                       //保存上一次偏差 
	 return pid->results;
}

const PIDBase PID = {
	Init,
	PositionPID,
};


