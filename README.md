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

Integrated ClosedXML for generating Excel reports.

Ready for extension to full-featured reporting.

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

Clear separation of concerns: domain models, data providers, and UI.

Flexible storage architecture — seamlessly switch between XML, JSON, or database solutions.
```text
TenderProjectV1/
├── Model/
│   ├── BusinessDomain/        # Core domain models (TenderInfo, ProcedureInfo, Customer)
│   └── System/                # System settings and statuses
│
├── DataProviders/             # XML & JSON storage implementations
├── Utilities/                 # Excel export via ClosedXML
├── HighlightTextBlockControl/ # Custom control for search highlighting
└── TenderListItem/            # Custom list item prototype
---

🚀 Current Status & Next Steps

✅ Core domain models designed (TenderInfo, ProcedureInfo, Customer)

✅ XML storage provider fully implemented

✅ JSON storage provider prototype added

✅ Smart search with inline highlighting

⚠️ Excel export: functional prototype

⏸ Development paused — preparing for migration to a full-featured web platform.
