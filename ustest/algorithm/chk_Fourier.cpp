#include "alice.h"
#include "arith.h"
#include "stdio.h"
extern "C"
{
#include "algorithm/frequency/Fourier.h"
}

#define PI _VAL_PI

float* SIN_TAB;
int FFT_N = 4096; 

void create_sin_tab(float* sin_t, int PointNum)
{
    int i;
    SIN_TAB = sin_t;
    FFT_N = PointNum;
    for (i = 0; i <= FFT_N / 4; i++)
        SIN_TAB[i] = sin(2 * PI * i / FFT_N);
}
double sin_tab(double pi)
{
    int n = 0;
    float a = 0;
    n = (int)(pi * FFT_N / 2 / PI);

    while (pi > 2 * PI) pi -= 2 * PI;
    while (pi < 0) pi += 2 * PI;

    if (n >= 0 && n <= FFT_N / 4)
        a = SIN_TAB[n];
    else if (n > FFT_N / 4 && n < FFT_N / 2)
    {
        n -= FFT_N / 4;
        a = SIN_TAB[FFT_N / 4 - n];
    }
    else if (n >= FFT_N / 2 && n < 3 * FFT_N / 4)
    {
        n -= FFT_N / 2;
        a = -SIN_TAB[n];
    }
    else if (n >= 3 * FFT_N / 4 && n < 3 * FFT_N)
    {
        n = FFT_N - n;
        a = -SIN_TAB[n];
    }

    return a;
}


#define NPT 4096
float my_sin_tab[NPT / 4 + 1];

#define F_s NPT
dfloat a[4096];
int main() {
    create_sin_tab(my_sin_tab, NPT);
    for0(i, numsof(a)) {
        a[i].real = dblsin(2 * PI * i / (F_s) * 10) + 
            dblsin(2 * PI * i / (F_s) * 8);
        a[i].imag = 0.0;
    }
    dflFourier(a, intlog2_iexpo(4096), sin_tab);
    for0(i, numsof(a)/2) {
        float out = dflabs_m(a[i]) / 4096;
        if (out > 0.01) printf("[%llu] %f\n", i, out);
    }
	return 0;
}