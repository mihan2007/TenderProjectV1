TenderProjectV1

Desktop prototype for tender documentation automation
WPF-based application for analyzing procurement documents, managing tender data, and preparing commercial proposals.
Status: development paused — migrating to a modern web-based version.

✨ Features

📂 Tender Data Management

Full tender profiles: procedure details, customer info, contacts, platform, timelines, amounts, guarantees.

Dual storage support: data can be stored and managed in both XML and JSON formats.

🔍 Smart Search with Highlighting

Search across tender names, INN, KPP, OGRN, contacts, and more.

Real-time highlighting of matching results.

📝 Editing Forms

Built-in forms for editing tender details and customer profiles.

Add, update, and delete entries seamlessly.

📊 Excel Export (Prototype)

Ready-to-use ClosedXML integration for generating Excel reports.

Flexible design to expand into full reporting later.

⚙️ System Configuration

Manage tender statuses and system parameters via JSON-based configuration files.

🛠️ Tech Stack

Platform: WPF, .NET Core 3.1

Language: C#

Libraries & Tools:

ClosedXML — Excel file generation

System.Text.Json — JSON serialization

XmlSerializer — XML data storage

Architecture:

Separation of concerns: domain models, data providers, and UI.

Flexible storage architecture — easily switch between XML, JSON, or databases in the future.

📂 Project Structure
├── 📁 Model/ # Data models
│ ├── 📁 BusinessDomain/ # Core domain models:
│ │ ├── TenderInfo.cs # Main tender entity
│ │ ├── ProcedureInfo.cs # Procedure details (dates, prices, guarantees, etc.)
│ │ └── Customer.cs # Customer details (name, INN, KPP, OGRN, contacts)
│ └── 📁 System/ # System settings and statuses
│ └── SystemSettings.cs # Configuration & tender statuses
│
├── 📁 DataProviders/ # Storage implementations
│ ├── DataProviderBase.cs # Abstract data provider
│ ├── XmlDataProvider.cs # XML storage support
│ └── JsonDataProvider.cs # JSON storage prototype
│
├── 📁 Utilities/ # Utility modules
│ └── ExcelExporter.cs # Excel export via ClosedXML
│
├── 📁 HighlightTextBlockControl/ # Custom WPF control
│ └── HighlightTextBlock.cs # Search result highlighting
│
└── 📁 TenderListItem/ # Custom list item prototype
└── ListTenderItem.xaml # WPF user control

🚀 Current Status & Next Steps

✅ Core domain model designed (TenderInfo, ProcedureInfo, Customer)

✅ XML storage provider fully implemented

✅ JSON storage provider prototype added

✅ Smart search with inline highlighting

⚠️ Excel export: functional prototype

⏸ Development paused — preparing for migration to a full-featured web platform.
