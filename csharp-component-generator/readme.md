# test locally
dotnet run

## requirements

wasm-tools installed (currently requires build from master `cargo install wasm-tools --git https://github.com/bytecodealliance/wasm-tools --branch main`)

# use as tool
dotnet pack
dotnet tool install --global --add-source ./nupkg csharp-component-generator
export PATH="$PATH:/home/jstur/.dotnet/tools"
dotnet component

# update for test
./install-tool.sh