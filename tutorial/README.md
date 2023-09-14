# Building a Calculator of Wasm Components

This tutorial walks through how to compose a component to build a Wasm calculator.
The WIT package for the calculator consists of a world for each mathematical operator
add an `op` enum that delineates each operator. The following example interface only
has an `add` operation:

```wit
package docs:calculator@0.1.0

interface calculate {
    enum op {
        add,
    }
    eval-expression: func(op: op, x: u32, y: u32) -> u32
}

interface add {
    add: func(a: u32, b: u32) -> u32
}

world adder {
    export add
}

world calculator {
    export calculate
    import add
}
```

To expand the exercise to add more components, add another operator world, expand the enum, and modify the `command` component to call it.

## Building and running the example

To compose a calculator component with an add operator, run the following:

```sh
(cd calculator && cargo component build --release)
(cd adder && cargo component build --release)
(cd command && cargo component build --release)
wasm-tools compose calculator/target/wasm32-wasi/release/calculator.wasm -d adder/target/wasm32-wasi/release/adder.wasm -o composed.wasm
wasm-tools compose command/target/wasm32-wasi/release/command.wasm -d composed.wasm -o command.wasm

# dotnet 7
(cd Adder_CS && dotnet build)
wasm-tools component embed --world adder wit/calculator.wit Adder_CS/bin/Debug/net7.0/Adder_CS.wasm -o main.embed.wasm 
wasm-tools component new main.embed.wasm --adapt wasi_snapshot_preview1.reactor.wasm -o main.component.wasm
wasm-tools validate main.component.wasm --features component-model
wasm-tools compose calculator/target/wasm32-wasi/release/calculator.wasm -d main.component.wasm -o composed.wasm
wasm-tools compose command/target/wasm32-wasi/release/command.wasm -d composed.wasm -o command.wasm


# dotnet 8
wasm-tools component embed --world adder wit/calculator.wit Adder_CS/bin/Debug/net8.0/wasi-wasm/AppBundle/Adder_CS.wasm -o main.embed.wasm 
```

this requires this wasmtime https://github.com/bytecodealliance/wasmtime/commit/134dddc built by cargo build --features component-model (don't forget git submodule update --init) and the snapshot preivew files from https://github.com/bytecodealliance/cargo-component/commit/822308cd2cd87cae6c766983d5619b17898f6dbc (which should match the version of cargo component you have otherwise you will get errors see https://bytecodealliance.zulipchat.com/#narrow/stream/327223-wit-bindgen/topic/wit-bindgen.20cs.20and.20wasm.20tools )

Now, run the component with wasmtime:

```sh
wasmtime run --wasm-features component-model command.wasm 1 2 add
1 + 2 = 3
```
