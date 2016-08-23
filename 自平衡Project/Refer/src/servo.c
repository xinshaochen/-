#include "servo.h"
#include "sys.h"

static void Init(void)
{
	RCC->APB1ENR|=1<<2;  
	RCC->APB2ENR|=1<<3;
	GPIOB->CRL&=0XF0FFFFFF;
	GPIOB->CRL|=0X0B000000;	 
	   

	TIM4->ARR=19999;
	TIM4->PSC=72;
	
	TIM4->CCMR1|=7<<4;
	TIM4->CCMR1|=1<<3;
	TIM4->CCER|=1<<0;
	TIM4->CR1=0x0080;
	TIM4->CR1|=0x01;
}
static void SetVal(u8 val)
{
	PWM_VAL=20000-(500+(val*20));
}


const ServoBase servo=
{
	Init,
	SetVal,
};


