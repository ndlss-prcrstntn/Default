using Default.Abstractions;
using Serilog.Core;
using Serilog.Events;

namespace Default.Logs;

public class TracedObjectDestructuringPolicy : IDestructuringPolicy
{
    public bool TryDestructure(
        object value,
        ILogEventPropertyValueFactory propertyValueFactory,
        out LogEventPropertyValue result)
    {
        if (value is ITraceEntity traced)
        {
            var traceDict = traced.GetTrace();

            var properties = traceDict
                .Select(kv => new LogEventProperty(kv.Key, new ScalarValue(kv.Value ?? "<null>")))
                .ToList();

            result = new StructureValue(properties);
            return true;
        }

        result = null!;
        return false;
    }
}