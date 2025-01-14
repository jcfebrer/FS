﻿using System;

namespace FSIACore.GenerativeNetwork
{
    public static class ActivationFunctions
    {
        public static double ReLU(double x) => Math.Max(0, x);

        public static double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));

        public static double Tanh(double x) => Math.Tanh(x);

        public static double Linear(double x) => x;
    }
}
