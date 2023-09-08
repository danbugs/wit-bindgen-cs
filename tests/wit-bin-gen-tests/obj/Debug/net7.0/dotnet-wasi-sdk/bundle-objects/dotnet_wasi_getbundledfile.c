#include <string.h>

int mono_wasm_add_assembly(const char* name, const unsigned char* data, unsigned int size);

extern const unsigned char wit_bin_gen_tests_dll_43D29EBC_o[];
extern const int wit_bin_gen_tests_dll_43D29EBC_o_len;
extern const unsigned char System_Console_dll_3132F191_o[];
extern const int System_Console_dll_3132F191_o_len;
extern const unsigned char System_Runtime_InteropServices_JavaScript_dll_3A6CC1BD_o[];
extern const int System_Runtime_InteropServices_JavaScript_dll_3A6CC1BD_o_len;
extern const unsigned char System_Threading_Channels_dll_E6A70678_o[];
extern const int System_Threading_Channels_dll_E6A70678_o_len;
extern const unsigned char System_Collections_Concurrent_dll_6C2F0293_o[];
extern const int System_Collections_Concurrent_dll_6C2F0293_o_len;
extern const unsigned char System_Private_CoreLib_dll_1185BB40_o[];
extern const int System_Private_CoreLib_dll_1185BB40_o_len;
extern const unsigned char System_Threading_ThreadPool_dll_694F488F_o[];
extern const int System_Threading_ThreadPool_dll_694F488F_o_len;
extern const unsigned char System_Collections_dll_1F9E9404_o[];
extern const int System_Collections_dll_1F9E9404_o_len;
extern const unsigned char System_Threading_dll_3A31529A_o[];
extern const int System_Threading_dll_3A31529A_o_len;
extern const unsigned char System_Runtime_dll_E28774D3_o[];
extern const int System_Runtime_dll_E28774D3_o_len;
extern const unsigned char System_Private_Uri_dll_D75A711A_o[];
extern const int System_Private_Uri_dll_D75A711A_o_len;
extern const unsigned char System_Threading_Thread_dll_BDB8B1EA_o[];
extern const int System_Threading_Thread_dll_BDB8B1EA_o_len;
extern const unsigned char System_Memory_dll_7C30E718_o[];
extern const int System_Memory_dll_7C30E718_o_len;
extern const unsigned char System_Runtime_InteropServices_dll_02FA4466_o[];
extern const int System_Runtime_InteropServices_dll_02FA4466_o_len;

const unsigned char* dotnet_wasi_getbundledfile(const char* name, int* out_length) {
  return NULL;
}

void dotnet_wasi_registerbundledassemblies() {
  mono_wasm_add_assembly ("wit-bin-gen-tests.dll", wit_bin_gen_tests_dll_43D29EBC_o, wit_bin_gen_tests_dll_43D29EBC_o_len);
  mono_wasm_add_assembly ("System.Console.dll", System_Console_dll_3132F191_o, System_Console_dll_3132F191_o_len);
  mono_wasm_add_assembly ("System.Runtime.InteropServices.JavaScript.dll", System_Runtime_InteropServices_JavaScript_dll_3A6CC1BD_o, System_Runtime_InteropServices_JavaScript_dll_3A6CC1BD_o_len);
  mono_wasm_add_assembly ("System.Threading.Channels.dll", System_Threading_Channels_dll_E6A70678_o, System_Threading_Channels_dll_E6A70678_o_len);
  mono_wasm_add_assembly ("System.Collections.Concurrent.dll", System_Collections_Concurrent_dll_6C2F0293_o, System_Collections_Concurrent_dll_6C2F0293_o_len);
  mono_wasm_add_assembly ("System.Private.CoreLib.dll", System_Private_CoreLib_dll_1185BB40_o, System_Private_CoreLib_dll_1185BB40_o_len);
  mono_wasm_add_assembly ("System.Threading.ThreadPool.dll", System_Threading_ThreadPool_dll_694F488F_o, System_Threading_ThreadPool_dll_694F488F_o_len);
  mono_wasm_add_assembly ("System.Collections.dll", System_Collections_dll_1F9E9404_o, System_Collections_dll_1F9E9404_o_len);
  mono_wasm_add_assembly ("System.Threading.dll", System_Threading_dll_3A31529A_o, System_Threading_dll_3A31529A_o_len);
  mono_wasm_add_assembly ("System.Runtime.dll", System_Runtime_dll_E28774D3_o, System_Runtime_dll_E28774D3_o_len);
  mono_wasm_add_assembly ("System.Private.Uri.dll", System_Private_Uri_dll_D75A711A_o, System_Private_Uri_dll_D75A711A_o_len);
  mono_wasm_add_assembly ("System.Threading.Thread.dll", System_Threading_Thread_dll_BDB8B1EA_o, System_Threading_Thread_dll_BDB8B1EA_o_len);
  mono_wasm_add_assembly ("System.Memory.dll", System_Memory_dll_7C30E718_o, System_Memory_dll_7C30E718_o_len);
  mono_wasm_add_assembly ("System.Runtime.InteropServices.dll", System_Runtime_InteropServices_dll_02FA4466_o, System_Runtime_InteropServices_dll_02FA4466_o_len);
}

