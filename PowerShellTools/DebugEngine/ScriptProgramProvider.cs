using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using log4net;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using Microsoft.VisualStudio.Shell;

namespace PowerShellTools.DebugEngine
{
    // This class implments IDebugProgramProvider2. 
    // This registered interface allows the session debug manager (SDM) to obtain information about programs 
    // that have been "published" through the IDebugProgramPublisher2 interface.
    [ComVisible(true)]
    [ProvideClass]
    [Guid("08F3B557-C153-4F6C-8745-227439E55E79")]
    public class ScriptProgramProvider : IDebugProgramProvider2
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ScriptProgramProvider));

        #region Implementation of IDebugProgramProvider2

        public int GetProviderProcessData(enum_PROVIDER_FLAGS Flags, IDebugDefaultPort2 pPort, AD_PROCESS_ID ProcessId, CONST_GUID_ARRAY EngineFilter, PROVIDER_PROCESS_DATA[] pProcess)
        {
            Log.Debug("ProgramProvider: GetProviderProcessData");
            return VSConstants.E_NOTIMPL;
        }

        public int GetProviderProgramNode(enum_PROVIDER_FLAGS Flags, IDebugDefaultPort2 pPort, AD_PROCESS_ID ProcessId, ref Guid guidEngine, ulong programId, out IDebugProgramNode2 ppProgramNode)
        {
            Log.Debug("ProgramProvider: GetProviderProgramNode");
            ppProgramNode = null;
            return VSConstants.S_OK;
        }

        public int WatchForProviderEvents(enum_PROVIDER_FLAGS Flags, IDebugDefaultPort2 pPort, AD_PROCESS_ID ProcessId, CONST_GUID_ARRAY EngineFilter, ref Guid guidLaunchingEngine, IDebugPortNotify2 pEventCallback)
        {
            Log.Debug("ProgramProvider: WatchForProviderEvents");

            return VSConstants.S_OK;
        }

        // Establishes a locale for any language-specific resources needed by the DE. This engine only supports Enu.
        int IDebugProgramProvider2.SetLocale(ushort wLangID)
        {
            Log.Debug("ProgramProvider: SetLocale");
            return VSConstants.S_OK;
        }


        #endregion
    }
}
