#ifndef _Motor_H_
#define _Motor_H_

#include "mcuhead.h"

#define PWMA   TIM1->CCR1
#define RDIR PBout(13)
#define LDIR   PBout(12)
#define PWMB   TIM1->CCR4

typedef struct {
	void(*Init)(void);
	void (*setSpeed)(int Lmoto,int Rmoto);
}MotorBase;

extern const MotorBase Motor;

#endif
