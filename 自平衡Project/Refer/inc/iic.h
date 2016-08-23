#ifndef _IIC_H_
#define _IIC_H_
#include "stm32f10x.h"
#include "sys.h"
#include "delay.h"

//=========================
#define SDA_IN()  {GPIOB->CRH&=0X0FFFFFFF;GPIOB->CRH|=8<<28;}
#define SDA_OUT() {GPIOB->CRH&=0X0FFFFFFF;GPIOB->CRH|=3<<28;}


#define IIC_SCL    PBout(14) //SCL
#define IIC_SDA    PBout(15) //SDA	 
#define READ_SDA   PBin(15)  //ÊäÈëSDA 


typedef struct
{
  void (*Init)(void);
  void (*Start)(void);
  void (*Stop)(void);
  u8 (*Wait_Ack)(void);
  void (*Ack)(void);
  void (*NAck)(void);
  void (*Send_Byte)(u8 txd);
  u8 (*Read_Byte)(unsigned char ack);
	unsigned char (*ReadOneByte)(unsigned char I2C_Addr,unsigned char addr);
}IICBase;
extern const IICBase IIC;

#endif
