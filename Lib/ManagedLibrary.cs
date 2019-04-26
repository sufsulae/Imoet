
#if IMOET_INCLUDE_LIBRARYMANAGER
namespace Imoet.Lib
{
    using System;
    using System.IO;
    using System.Reflection;
    using Imoet.Utility;
    [Serializable]
    public class ManagedLibrary {
        private Assembly m_assembly;
        public void LoadLibrary(Stream assemblyFile) {
            m_assembly = Assembly.Load(MemoryUtil.GetStreamBytes(assemblyFile));
        }
    }
}
#endif