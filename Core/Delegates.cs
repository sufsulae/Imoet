//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    public delegate TR ImoetDelegateReturn<TR>();
    public delegate TR ImoetDelegateReturn<TR, T1>(T1 val1);
    public delegate TR ImoetDelegateReturn<TR, T1, T2>(T1 val1, T2 val2);
    public delegate TR ImoetDelegateReturn<TR, T1, T2, T3>(T1 val1, T2 val2, T3 val3);
    public delegate TR ImoetDelegateReturn<TR, T1, T2, T3, T4>(T1 val1, T2 val2, T3 val3, T4 val4);
    public delegate TR ImoetDelegateReturn<TR, T1, T2, T3, T4, T5>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5);

    public delegate void ImoetDelegateRef<T1>(ref T1 val1);
    public delegate void ImoetDelegateRef<T1, T2>(ref T1 val1, ref T2 val2);
    public delegate void ImoetDelegateRef<T1, T2, T3>(ref T1 val1, ref T2 val2, ref T3 val3);
    public delegate void ImoetDelegateRef<T1, T2, T3, T4>(ref T1 val1, ref T2 val2, ref T3 val3, ref T4 val4);
    public delegate void ImoetDelegateRef<T1, T2, T3, T4, T5>(ref T1 val1, ref T2 val2, ref T3 val3, ref T4 val4, ref T5 val5);

    public delegate void ImoetDelegateOut<T1>(out T1 val1);
    public delegate void ImoetDelegateOut<T1, T2>(out T1 val1, out T2 val2);
    public delegate void ImoetDelegateOut<T1, T2, T3>(out T1 val1, out T2 val2, out T3 val3);
    public delegate void ImoetDelegateOut<T1, T2, T3, T4>(out T1 val1, out T2 val2, out T3 val3, out T4 val4);
    public delegate void ImoetDelegateOut<T1, T2, T3, T4, T5>(out T1 val1, out T2 val2, out T3 val3, out T4 val4, out T5 val5);

    public delegate void ImoetAction();
    public delegate void ImoetAction<T1>(T1 val1);
    public delegate void ImoetAction<T1, T2>(T1 val1, T2 val2);
    public delegate void ImoetAction<T1, T2, T3>(T1 val1, T2 val2, T3 val3);
    public delegate void ImoetAction<T1, T2, T3, T4>(T1 val1, T2 val2, T3 val3, T4 val4);
    public delegate void ImoetAction<T1, T2, T3, T4, T5>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5);
}
