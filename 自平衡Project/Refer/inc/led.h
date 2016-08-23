#ifndef __LED_H
#define __LED_H	 
#include "sys.h"

#define LED1 PBout(5)


typedef struct
{
	void (*Init)(void);
}LEDBase;
extern const LEDBase led;


#endif
