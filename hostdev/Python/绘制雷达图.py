import numpy as np
import pandas as pd
import matplotlib.pyplot as plt

plt.rcParams['font.family']='SimHei' 
plt.rcParams['font.sans-serif']='SimHei'

dataset_0 = pd.DataFrame(data=[1, 1, 1, 1, 1, 1],
            index=['复习旧知识','刷新的动态','欣赏旧资源','学习新知识','整理收藏夹','获取新资源'])
dataset_1 = pd.DataFrame(data=[1, 1, 1, 1, 1, 1],
            index=['工作','社会','学习','休息','生存','娱乐'])

radar_labels_0=dataset_0.index
radar_labels_1=dataset_1.index
nAttr=6
data_0=dataset_0.values
data_1=dataset_1.values

angles=np.linspace(0,2*np.pi,nAttr,endpoint= False)
data_0=np.concatenate((data_0, [data_0[0]])) 
data_1=np.concatenate((data_1, [data_1[0]]))
angles=np.concatenate((angles, [angles[0]]))

fig=plt.figure(facecolor="white",figsize=(10,6))

plt.subplot(121, polar=True)
plt.plot(angles,data_0,'o-',
         linewidth=1.5, alpha= 0.2)
plt.fill(angles,data_0, alpha=0.25) 
plt.thetagrids(angles[:-1]*180/np.pi,radar_labels_0,1.2) 
plt.title('内耗')

plt.subplot(122, polar=True)
plt.plot(angles,data_1,'o-',
         linewidth=1.5, alpha= 0.2)
plt.fill(angles,data_1, alpha=0.25) 
plt.thetagrids(angles[:-1]*180/np.pi,radar_labels_1,1.2)
plt.title('外耗')

plt.tight_layout()
plt.figtext(0.52, 0.95,'XXXX年XX月XX日', ha='center', size=20)

plt.savefig('2024Feb01.png')
plt.show()
