using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestApp.Controllers
{
    /// <summary>
    /// Это пример калькулятора с базовыми функциями:
    /// сложения, вычитания, деления, умножения целых чисел
    /// реальное приложение намного сложнее, например
    /// числа могут быть не целыми, проверка на выход допустимых диапазонов...
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {

        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Sub(int a, int b)
        {
            return a - b;
        }

        public int Div(int a, int b)
        {
            if(b == 0)
            {
                throw new DivideByZeroException();
            }
            return a / b;
        }

        public int Mul(int a, int b)
        {
            return a * b;
        }
    }
}