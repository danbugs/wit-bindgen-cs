.PHONY: install
install: 
	(cd ./csharp-component-generator && ./install-tool.sh)

.PHONY: generate
generate: 
	(cd ./MyProject && dotnet component generate --wit ../adder.wit)

.PHONY: build
build: 
	(cd ./MyProject && dotnet component build --world adder --wit ../adder.wit --preview ../wasi_snapshot_preview1.reactor.wasm)

.PHONY: demo
demo: install
	# do not forget to update readme.md demo script files
	rm -rf MyProject
	mkdir MyProject
	(cd ./MyProject && dotnet new console)
	(cd ./MyProject && dotnet component init)
	(cp ./Program.cs ./MyProject/Program.cs)
	$(MAKE) generate
	$(MAKE) build
	./build.sh
	wasmtime run --wasm-features component-model final.wasm 1 2 add
