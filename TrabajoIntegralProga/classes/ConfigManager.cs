using System;

public sealed class ConfigManager
{
    public ConfigManager()
    {
        // Singleton de config (Etapa 2) 
        private static readonly Lazy<ConfigManager>
        _inst = new(() => new ConfigManager());
    public static ConfigManager Instance => _inst.Value;
    private ConfigManager() { }
    public decimal EnvioGratisDesde { get; set; } = 50000m;
    public decimal IVA { get; set; } = 0.21m; // usado por decorator en Etapa 3
}
}
