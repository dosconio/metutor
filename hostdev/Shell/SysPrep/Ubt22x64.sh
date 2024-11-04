# UTF-8 Bash LF TAB4
mkdir -p ~/tmp
#[manual] setup VM Tools
echo 提示：VMware共享文件夹 sudo vmhgfs-fuse .host:/ /mnt/hgfs -o subtype=vmhgfs-fuse,allow_other

sudo apt remove unattended-upgrades

# set Moebuntu in 'User Theme'
sudo apt install gnome-shell-extension-manager
sudo apt install gnome-tweaks
# - {TODO}解压到 /usr/share/themes
echo 请手动设置UBUNTU主题！：gnome-tweaks

echo 切换清华源
sudo mv /etc/apt/sources.list /etc/apt/sources.old
sudo cp Ubt22x64/tuna.list /etc/apt/sources.list
sudo apt update

echo setup VSCode
sudo snap install --classic code
sudo apt install git

echo 请手动安装 VPN

sudo apt install make
sudo apt install build-essential
sudo apt install gcc
sudo apt-get install gcc-multilib g++-multilib # so you can use -m32 or -m64
sudo apt install perl
sudo apt install python3
sudo apt-get install pkg-config

echo "export uincpath=/mnt/hgfs/SVGN/unisym/inc" | tee -a ~/.bashrc>/dev/null
echo "export ulibpath=/mnt/hgfs/SVGN/unisym/lib" | tee -a ~/.bashrc>/dev/null
echo "export ubinpath=/mnt/hgfs/_bin"  | tee -a ~/.bashrc>/dev/null

sudo curl --proto '=https' --tlsv1.2 https://sh.rustup.rs -sSf | sh
sudo apt install cargo
rustup install nightly
rustup target add riscv64gc-unknown-none-elf
cargo install cargo-binutils
rustup component add llvm-tools-preview
rustup component add rust-src
rustup target add x86_64-unknown-linux-musl

# 用于编译 Linux 内核，需要：
# sudo apt install libncurses5-dev linux-source
sudo apt install flex bison
# sudo apt install libelf-dev
# sudo apt install libffi-dev libssl-dev libxml2-dev libxslt1-dev libjpeg8-dev
sudo apt-get install zlib1g zlib1g-dev

sudo apt-get install qemu-system
# : 以下为编译 qemu 源码所用的依赖
# sudo apt-get install ninja-build
# sudo apt-get install libglib2.0-dev
# sudo apt-get install libpixman-1-dev

sudo apt-get install libcanberra-gtk-module

# 用于 verilator
sudo apt install autoconf ccache
sudo apt install libgoogle-perftools-dev numactl perl-doc
sudo apt install help2man

# Ubuntu only (ignore if gives error)
sudo apt install libfl2 libfl-dev # zlibc

# YSYX
sudo apt install vim
sudo apt install man
sudo apt install gcc-doc
sudo apt install gdb
sudo apt install libreadline-dev
sudo apt install libsdl2-dev
sudo apt install llvm
sudo apt install llvm-dev
sudo apt install llvm-11
sudo apt install llvm-11-de

# FUZZ on ARM
sudo apt install gcc-arm-linux-gnueabihf # gcc for ARM
sudo apt install libc6-armhf-cross # the libc library for ARM
sudo apt install gdb gdb-multiarch
sudo apt install gcc-mips-linux-gnu
sudo apt install libc6-mips-cross

# MCU8051
sudo apt install sdcc
sudo apt install python3-pip
pip3 install stcgal

# MCU on ARM
sudo apt install gcc-arm-none-eabi # 2,575 MB
sudo apt install stm32flash
sudo apt install minicom
sudo apt-get install libusb-1.0
sudo apt install stlink-tools
sudo apt install stlink-gui
