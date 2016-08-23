#ifndef _PID_H_
#define _PID_H_

#include"mcuhead.h"

typedef struct {
	float P;
	float I;
	float D;
	float Bias;//偏差
	float Integral_Bias;//偏差的积分
	float Last_Bias;//上一次的偏差
	float results;//输出结果
}PIDStruct;

typedef struct {
	void(*Init)(PIDStruct *pid, float P, float I, float D);
	s16 (*PositionPID)(PIDStruct *pid,s16 Now,s16 Target);
}PIDBase;

extern const PIDBase PID;

#endif
