﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("BromineCore")]
[assembly: AssemblyProduct("Bromine Core")]
[assembly: AssemblyCopyright("Copyright ©  2019")]
[assembly: ComVisible(false)]
[assembly: Guid("bb346a0f-1dd9-49ef-8d1f-da87b91963cd")]
[assembly: AssemblyVersion("1.0.0.3")]

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
[assembly: InternalsVisibleTo("Tests.Bromine")]
