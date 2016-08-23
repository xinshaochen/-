#include "led.h"


static void Init(void)
{
  RCC->APB2ENR|=1<<3;				 
	GPIOB->CRL&=0XFF0FFFFF;
	GPIOB->CRL|=0X00300000;	 
}

const LEDBase led=
{
	Init,
};

