# wit-bindgen-cs

This is the start of C# bindings for [wit](https://github.com/bytecodealliance/wit).

This is a project started at ["Componentize The World" – a hackathon hosted by the Bytecode Alliance](https://www.eventbrite.com/e/bytecode-alliance-componentize-the-world-tickets-681895717447), and was continued at [Microsoft's internal hackathon](https://hackbox.microsoft.com/hackathons/hackathon2023/project/52132) for the period of one week. With that, *it's very much a work in progress and, for now, only works with very specific WIT files (i.e., 1 interface exposing 1 function)*.

## How to use it?

One of our main focuses was the developer experience, where we hoped to integrate with existing dotnet tooling and abstract away direct interactions w/ `wit-bindgen`, and `wasm-tools`.

### Prerequisites

- .NET 8 Preview 4 or later
    - This can be downloaded from [Download .NET 8.0 (Linux, macOS, and Windows)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
    - Verify that `dotnet --version` tells you it's .NET 8 Preview 4 or later
- WASI SDK
    - Download from [wasi-sdk releases page](https://github.com/WebAssembly/wasi-sdk/releases). If you're using Windows, you need the one with `mingw` in its name.
    - Create an environment variable called `WASI_SDK_PATH` giving the path where you extracted the WASI SDK download, i.e., the directory containing `bin`/`lib`/`share`.
- Rust tooling (i.e., cargo, cargo component, etc.) for demo. purposes.
    - Install from [rustup.rs](https://rustup.rs/)
    - Install from [cargo-component](https://github.com/bytecodealliance/cargo-component.git)
- wasmtime 13.0.0
    - Install from [here](https://github.com/bytecodealliance/wasmtime/commit/134dddc) by running `git submodule update --init && cargo build --features component-model` (don't forget git submodule update --init).


> Note: 
>> - `wit-bindgen`, and `wasm-tools` are automatically installed with `dotnet component init`.
>> - preview reactor files can be found [here](https://github.com/bytecodealliance/cargo-component/commit/822308cd2cd87cae6c766983d5619b17898f6dbc), but are already included in this repo..

## Running the demo

This demo. utilizes the work upstream from the [component-model tutorial](https://github.com/bytecodealliance/component-docs/tree/main/component-model/examples/tutorial), but replaces one of the components w/ a C# one.

```sh
(cd ./csharp-component-generator && ./install-tool.sh)
# ^^^ install the dotnet plugin
rm -rf MyProject
# ^^^ remove any existing project directory
mkdir MyProject
# ^^^ create a new project directory
(cd ./MyProject && dotnet new console)
# ^^^ create a new console project
(cd ./MyProject && dotnet component init)
# ^^^ initialize the project for use with wasm-tools
(cp ./Program.cs ./MyProject/Program.cs)
# ^^^ copy the Program.cs with Add impl. into the project directory
(cd ./MyProject && dotnet component generate --wit ../adder.wit)
# ^^^ generate the C# bindings for the WIT file
(cd ./MyProject &&  dotnet component build --world adder --wit ../adder.wit --preview ../wasi_snapshot_preview1.reactor.wasm)
# ^^^ build the project
./build.sh
# ^^^ build the Rust components to be composed with the C# component
wasmtime run --wasm-features component-model final.wasm 1 2 add
# ^^^ run the composed component
```

## Video demo

[![](https://i.imgur.com/cagFD38.png)](https://streamable.com/i6f0vl)