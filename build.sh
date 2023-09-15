set -e

(cd ./demo-rust-components/calculator && cargo component build --release)
(cd ./demo-rust-components/command && cargo component build --release)
wasm-tools validate ./MyProject/main.component.wasm --features component-model
wasm-tools compose ./demo-rust-components/calculator/target/wasm32-wasi/release/calculator.wasm -d ./MyProject/main.component.wasm -o calculator-adder.wasm
wasm-tools compose ./demo-rust-components/command/target/wasm32-wasi/release/command.wasm -d calculator-adder.wasm -o final.wasm