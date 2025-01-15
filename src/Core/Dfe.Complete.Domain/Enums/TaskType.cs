﻿using System.ComponentModel;

namespace Dfe.Complete.Domain.Enums
{
    public enum TaskType
    {
        [Description("Conversion::TasksData")]
        Conversion = 1,
        [Description("Transfer::TasksData")]
        Transfer = 2
    }
}
