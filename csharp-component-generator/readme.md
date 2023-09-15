# test locally
dotnet run

## requirements

wasm-tools installed (currently requires build from master `cargo install wasm-tools --git https://github.com/bytecodealliance/wasm-tools --branch main`)

## use as tool
dotnet pack
dotnet tool install --global --add-source ./nupkg csharp-component-generator
export PATH="$PATH:/home/<your-name>/.dotnet/tools"
dotnet component

## update for test
./install-tool.sh

## json -> c#

```json
{
  "worlds": [
    {
      "name": "adder",
      "imports": {},
      "exports": {
        "interface-0": {
          "interface": 0
        }
      },
      "package": 0
    }
  ],
  "interfaces": [
    {
      "name": "add",
      "types": {},
      "functions": {
        "add": {
          "name": "add",
          "kind": "freestanding",
          "params": [
            {
              "name": "a",
              "type": "u32"
            },
            {
              "name": "b",
              "type": "u32"
            }
          ],
          "results": [
            {
              "type": "u32"
            }
          ]
        }
      },
      "package": 0
    }
  ],
  "types": [],
  "packages": [
    {
      "name": "docs:calculator@0.1.0",
      "interfaces": {
        "add": 0
      },
      "worlds": {
        "adder": 0
      }
    }
  ]
}
```