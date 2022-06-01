# imagesの中のjpgを透過pngに変換して出力するコード
# 透過処理は14行目。
import os
import cv2
import numpy as np
import glob
from PIL import Image

files = glob.glob("Assets/images/*.jpg")
for tmp in files:
    newPath = tmp[:-4] + '.png'

    src = cv2.imread(tmp)
    mask = np.all(src >= 50, axis=-1)
    dst = cv2.cvtColor(src, cv2.COLOR_BGR2BGRA)
    dst[mask,3] = 0
    cv2.imwrite(newPath, dst)

    os.remove(tmp)