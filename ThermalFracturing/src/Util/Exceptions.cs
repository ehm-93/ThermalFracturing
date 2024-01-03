using System;

namespace Ehm93.VintageStory.ThermalFracturing.Util;

class Exceptions
{
    public static Exception MissingProperty(string name)
    {
        return new InvalidOperationException($"The property named '{name}' is required but was not provided");
    }
}