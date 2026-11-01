# 1151VR-HW4-414262337-徐宇駿

## 1. 專案連結
* **GitHub 連結**：https://github.com/yujyunhsu-ui/1151VR-HW4-414262337--
* **YouTube 連結**：https://www.youtube.com/watch?v=kPg5MTWKYiM

---

## 2. 專案截圖
<img width="1917" height="1078" alt="image" src="https://github.com/user-attachments/assets/c4668456-1e9f-4d82-bb82-107eaf54fef6" />


---

## 3. 製作流程與相關操作說明

### 製作流程
1. **建立專案與載入模型**：在 Unity 6 開啟 3D 專案，匯入 P-38J 飛行器模型（`P38J Lightning`）與石頭障礙物模型（`HQBigRock`），並於前方設立終點線。
2. **場景碰撞與隨機生成機制**：
   * 為飛行器掛載 `Rigidbody` 與 `Box Collider`，設定物理邊界與無重力飛行。
   * 將石頭障礙物製作成 Prefab，配置碰撞器與後方通過判定區（`Is Trigger`），透過 `Spawner` 腳本在飛行路線上隨機生成障礙物；終點線則開啟 `Is Trigger` 作為過關判定。
3. **掛載三個音效**：透過 `AudioSource` 實作飛行期間持續播放的引擎背景音效、碰到石頭時觸發的撞擊音效，以及順利閃過障礙物與抵達終點時的通過音效。
4. **跨平台輸出與版本控制**：測試輸出至行動裝置遊玩，並建立公開 GitHub 儲存庫（套用 Unity `.gitignore`）上傳專案與錄製實機畫面至 YouTube。

### 相關操作
* **電腦操作**：使用鍵盤 `W`、`A`、`S`、`D` 或方向鍵控制飛機上下左右閃避隨機生成的石頭。
* **手機操作**：於螢幕觸控滑動控制飛機移動方向，順利閃避石頭並飛抵終點即完成通關。
