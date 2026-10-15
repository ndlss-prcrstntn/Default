using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Default.Abstractions;

namespace Default.Cache;

public class KeyDbConfigSection:IConfigSection
{
    public static readonly string Name = "keyDB";

    public string Connection { get; set; }
}