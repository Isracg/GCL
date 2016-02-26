﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semantic
{
    public class GclCodeGenerator
    {
        private readonly StringBuilder _builder;

        public GclCodeGenerator(int size)
        {
            _builder = new StringBuilder(size);
            
            _builder.Append("#include <iostream>\n" +
                            "#include <vector>\n" +
                            "#include <cuda_runtime.h>\n\n" +
                            "#pragma comment(lib, \"cudart\")\n\n" +
                            "using namespace std;\n");

            _builder.Append("\ndouble Pow(double _base, double _pow)\n" +
                            "{\n\tif(_pow <= 0) return 1;\n" +
                            "\tint i = 0;\n" +
                            "\tfor (i = 0; i < _pow; i++)\n" +
                            "\t\t_base *= _base;\n" +
                            "\treturn _base;\n}\n\n");

            _builder.Append("\n__device__ double D_Pow(double _base, double _pow)\n" +
                            "{\n\tif(_pow <= 0) return 1;\n" +
                            "\tint i = 0;\n" +
                            "\tfor (i = 0; i < _pow; i++)\n" +
                            "\t\t_base *= _base;\n" +
                            "\treturn _base;\n}\n\n");
        }

        public void AddCode(string partialCode)
        {
            _builder.Append(partialCode);
        }

        public void AddTabs(int tabs)
        {
            for (int i = 0; i < tabs; i++)
            {
                _builder.Append("\t");
            }
        }

        public void End()
        {
            var _debugString = _builder.ToString();
        }
    }
}