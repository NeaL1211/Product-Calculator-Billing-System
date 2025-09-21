# ProductCalculatorApp

一個用 C# Windows Forms 開發的小型進銷存/請款單系統，支援產品輸入、訂單管理、歷史查詢與列印功能。  
專案重點在於結合 **資料綁定 (DataBinding)** 與 **自訂列印版面設計**，模擬實務上的單據生成流程。

## 功能特色
- **訂單建立**：輸入產品、數量、單價，自動計算小計與工資。
- **歷史紀錄**：依日期、金額、客戶篩選訂單，並可檢視/修改明細。
- **報表列印**  
  - 月請款單  
  - 日出貨單  
  - 支援連續紙（9.5x5.5）與自訂頁尾簽名欄。  
- **產品搜尋**：可依產品編號快速查詢。

## 技術重點
- C# WinForms
- DataGridView + BindingList 資料綁定
- PrintDocument / PrintPreviewDialog 自訂列印
- 自訂紙張大小與無邊界列印
- 基本 MVC 架構：資料存放在 `DataStore`，UI 與邏輯分離

## 如何執行
1. 使用 Visual Studio 開啟方案。
2. 確保 .NET Framework / .NET 6+ 開發環境。
3. 執行 `app` 專案即可。

---

