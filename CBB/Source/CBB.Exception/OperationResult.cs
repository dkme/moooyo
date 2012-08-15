using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace CBB.ExceptionHelper
{
    public class OperationResult
    {
        public string err;
        public bool ok;

        public OperationResult(bool isOK, String errstr)
        {
            err = errstr;
            ok = isOK;
        }
        public OperationResult(bool isOK)
        {
            err = "";
            ok = isOK;
        }
    }
}