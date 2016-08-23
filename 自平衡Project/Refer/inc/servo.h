#ifndef __SERVO_H
#define __SERVO_H
#include "sys.h"

#define PWM_VAL TIM4->CCR1 

typedef struct
{
	void (*Init)();
	void (*SetVal)(u8 val);
}ServoBase;
extern const ServoBase servo;


#endif

