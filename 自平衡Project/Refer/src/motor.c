#include "Motor.h"
#include "encoder.h"
void SetSpeed(int Lmoto,int Rmoto);

static void Init()
{
	
	RCC->APB2ENR|=1<<3;
	GPIOB->CRH&=0XFF00FFFF;   //PORTB12 13推挽输出
	GPIOB->CRH|=0X00330000;
	
	RCC->APB2ENR|=1<<11;       //使能TIM1时钟    
	RCC->APB2ENR|=1<<2;        //PORTA时钟使能     
	GPIOA->CRH&=0XFFFF0FF0;    //PORTA8 11复用输出
	GPIOA->CRH|=0X0000B00B;
	TIM1->ARR=7199;             //设定计数器自动重装值 
	TIM1->PSC=71;             //预分频器不分频
	TIM1->CCMR2|=6<<12;        //CH4 PWM1模式	
	TIM1->CCMR1|=6<<4;         //CH1 PWM1模式	
	TIM1->CCMR2|=1<<11;        //CH4预装载使能	 
	TIM1->CCMR1|=1<<3;         //CH1预装载使能	  
	TIM1->CCER|=1<<12;         //CH4输出使能	   
	TIM1->CCER|=1<<0;          //CH1输出使能	
	TIM1->BDTR |= 1<<15;       //TIM1必须要这句话才能输出PWM
	TIM1->CR1=0x8000;          //ARPE使能 
	TIM1->CR1|=0x01;          //使能定时器1 		

	SetSpeed(0,0);
}

int myabs(int a)
{ 		   
	  int temp;
		if(a<0)  temp=-a;  
	  else temp=a;
	  return temp;
}

void SetSpeed(int Lmoto,int Rmoto)
{
		signed int bufLmoto,bufRmoto;
		signed int Amplitude=6000;  
	
			if(Lmoto>0)			LDIR=0;
		else 	          LDIR=1;
	
	if(Rmoto>0) RDIR=1;
	else RDIR=0;
	

    if(Lmoto<-Amplitude) 
		{
			Lmoto=-Amplitude;
		}
		if(Lmoto>Amplitude)
		{
			Lmoto=Amplitude;
		}
		if(Lmoto>0) Lmoto=Amplitude-Lmoto;
		else Lmoto=Amplitude+Lmoto;

	if(Rmoto<-Amplitude)
	{
		Rmoto=-Amplitude;
	}
	if(Rmoto>Amplitude)
	{
		Rmoto=Amplitude;
	}
	if(Rmoto>0) Rmoto=Amplitude-Rmoto;
		else Rmoto=Amplitude+Rmoto;
	

	
	
	bufLmoto = myabs(Lmoto);
	bufRmoto = myabs(Rmoto);
	
	if(bufLmoto>=Amplitude)
		bufLmoto=7199;
	
	if(bufRmoto>=Amplitude)
		bufRmoto=7199;
	
			PWMA=bufLmoto;
			PWMB=bufRmoto;	
}



const MotorBase Motor = {
	Init,	
	SetSpeed,
};
