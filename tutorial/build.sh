set -e

(cd calculator && cargo component build --release)
(cd ../my-project && dotnet component build --world adder --wit adder.wit --preview ../tutorial/wasi_snapshot_preview1.reactor.wasm)
(cd command && cargo component build --release)
wasm-tools validate ../my-project/main.component.wasm --features component-model
wasm-tools compose calculator/target/wasm32-wasi/release/calculator.wasm -d ../my-project/main.component.wasm -o composed.wasm
wasm-tools compose command/target/wasm32-wasi/release/command.wasm -d composed.wasm -o command.wasm