# test locally
dotnet run

## Install as tool
dotnet pack
dotnet tool install --global --add-source ./nupkg csharp-component-generator
export PATH="$PATH:/home/<your-name>/.dotnet/tools"
dotnet component --help

# use it
dotnet component init
dotnet component generate --wit adder.wit
dotnet component build --world adder --wit adder.wit --preview ../tutorial/wasi_snapshot_preview1.reactor.wasm

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