cd /home/ayano

sudo pacman -S git
git clone https://github.com/dosconio/unisym.git
mv unisym Unisym

sudo mkdir -p /mnt/bin /mnt/shared /mnt/her
sudo pacman -S virtualbox-guest-utils
sudo pacman -S linux-headers
sudo modprobe vboxsf
echo "share /mnt/shared vboxsf defaults 0 0" | sudo tee -a /etc/fstab
echo "_bin /mnt/bin vboxsf defaults 0 0" | sudo tee -a /etc/fstab
echo "SVGN /mnt/her vboxsf defaults 0 0" | sudo tee -a /etc/fstab
#自动挂载点 /mnt/bin

# .bashrc
export uincpath=/mnt/her/unisym/inc
export ulibpath=/mnt/her/unisym/lib
export uobjpath=/home/ayano/Object
export ubinpath=/mnt/bin
export PATH=/mnt/bin/ELF64:$PATH



