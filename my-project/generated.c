
#include <wasm/driver.h>
#include "adder.h"
#include <assert.h>


void mono_wasm_invoke_method_ref(MonoMethod* method, MonoObject** this_arg_in, void* params[], MonoObject** _out_exc, MonoObject** out_result);

int dotnet_started = 0;
void _start();
void ensure_dotnet_started() {
    if (!dotnet_started) {
        _start();
        dotnet_started = 1;
    }
}

uint32_t exports_docs_calculator_add_add(uint32_t a, uint32_t b)
{
    ensure_dotnet_started();

	MonoMethod* method = lookup_dotnet_method("my-project", "add", "addHelper", "Export_add", -1);
    void* method_params[] = { &a, &b };
    MonoObject* exception;
    MonoObject* result;
    mono_wasm_invoke_method_ref(method, NULL, method_params, &exception, &result);	
    assert(!exception);

	uint32_t res = *(uint32_t*)mono_object_unbox(result);
    return res;
}
