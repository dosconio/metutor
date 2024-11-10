
xsym:
	echo {TODO} make soft-link to unisym
check:
	@echo check UNISYM for RUST
	@cd $(ulibpath)/Rust/unisym && cargo test
check-m:
	@echo check Magice
	@bash $(ulibpath)/../magic/chkmgc.sh

kitwin:
	@# omit normal kits
	-ahkcc $(ulibpath)/Script/AutoHotkey/Arnscr.ahk $(ubinpath)/arnscr.exe

release-on-win:
	@echo build for UNISYM
	cd $(ulibpath)/.. && make cgw32
	cd $(ulibpath)/.. && make cgw64
	cd $(ulibpath)/.. && make cvw32
	cd $(ulibpath)/.. && make cvw64
	cd $(ulibpath)/.. && make rust
	cd $(ulibpath)/.. && make manual

release-on-lin:
	@echo build for UNISYM
	cd $(ulibpath)/.. && make cgl32
	cd $(ulibpath)/.. && make cgl64
	cd $(ulibpath)/.. && make mx86
	cd $(ulibpath)/.. && make rust
	

