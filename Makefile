
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

release-on-win
	@echo build for UNISYM
	make -f $(ulibpath)/../Makefile cgw32
	make -f $(ulibpath)/../Makefile cgw64
	make -f $(ulibpath)/../Makefile cvw32
	make -f $(ulibpath)/../Makefile cvw64
	make -f $(ulibpath)/../Makefile rust
	make -f $(ulibpath)/../Makefile manual

release-on-lin
	@echo build for UNISYM
	make -f $(ulibpath)/../Makefile cgl32
	make -f $(ulibpath)/../Makefile cgl64
	make -f $(ulibpath)/../Makefile mx86
	make -f $(ulibpath)/../Makefile rust
	

