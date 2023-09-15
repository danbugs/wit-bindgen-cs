set -e

(cd calculator && cargo component build --release)
(cd Adder_CS && dotnet build)
(cd command && cargo component build --release)
wasm-tools component embed --world adder wit/calculator.wit Adder_CS/bin/Debug/net8.0/wasi-wasm/AppBundle/Adder_CS.wasm -o main.embed.wasm 
wasm-tools component new main.embed.wasm --adapt wasi_snapshot_preview1.reactor.wasm -o main.component.wasm
wasm-tools validate main.component.wasm --features component-model
wasm-tools compose calculator/target/wasm32-wasi/release/calculator.wasm -d main.component.wasm -o composed.wasm
wasm-tools compose command/target/wasm32-wasi/release/command.wasm -d composed.wasm -o command.wasm