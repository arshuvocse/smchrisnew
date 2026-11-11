# Project Information

## Framework

- C#
- ASP.NET Web Application
- .NET Framework 4.5
- SQL Server
- IIS

---

## Architecture

- 3 Tier Architecture

Presentation Layer

↓

Business Layer

↓

Data Access Layer

↓

SQL Server

---

## Database

SQL Server

Use Existing Stored Procedures.

Never modify database structure unless requested.

---

## Coding Rules

Follow existing coding style.

Reuse existing Business Layer methods.

Reuse existing DAL methods.

Don't create duplicate methods.

Don't rename existing methods.

Don't rename database objects.

---

## SQL Rules

Use Stored Procedures whenever possible.

Use parameterized SQL.

Avoid SQL Injection.

Optimize queries.

---

## Before Coding

Always

Read existing code.

Understand business flow.

Explain implementation plan.

Modify minimum files.

---

## Error Handling

Use try-catch.

Return meaningful error message.

Don't swallow exceptions.

---

## Git Rules

Never commit automatically.

Never push automatically.

Ask before major refactoring.

---

## Performance

Avoid duplicate database calls.

Reuse existing objects.

Avoid unnecessary loops.

Optimize SQL queries.

---

## Coding Style

Follow existing project style.

Don't introduce new frameworks.

Don't upgrade .NET version.

Don't install unnecessary NuGet packages.

---

## AI Behavior

Always explain before editing.

Generate production-ready code.

Respect existing architecture.

Never modify unrelated code.

Always preserve backward compatibility.

---

## MCP Tools

### Headroom

- headroom_compress — compresses large content (file, tool output, logs) to save context space, returns a hash.
- headroom_retrieve — retrieves the original full content later using the hash.
- headroom_stats — shows session stats on how many tokens/cost were saved.