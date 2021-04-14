# AspNetCore2017-Practice
AspNetCore2017-Practice

## Code-First

- 코드작성 우선주의
- C# 코드작성 > DB
- Migrations : 미리 작성된 C# 코드로 DB의 테이블과 컬럼을 생성



- 장점
  - Table 과 Column을 Application에서 관리
  - Migrations를 통한 이력관리
- 단점
  - 사소한 작업을 Migrations 하는 것이 번거로움
  - 운영서버에 바로 적용이 어려움



## Data-Migration

add-migration AddingUserTables -project NetCore.Migrations

update-database -project NetCore.Migrations



## Database-First

- 데이터베이스 작업 우선주의
- 데이터베이스 선 작업 > C# 코드작성
- Entity Data Modelling
  - 코드를 손쉽게 작성할 수 있도록 도와줌



- 장점
  - Database 작업은 기존과 동일하게 수행가능
  - Entity Data Modeling으로 손코딩 거의 없음
- 단점
  - Database 작업의 이력관리를 하지 못함
  - Table 또는 Column 변경 시 C# 코드도 수동변경



## ExecuteSqlCommand

- 작업결과 int 값을 return
- Procedure 에서 Database의 Insert, Update, Delete 작업 후 Select 구문을 추가해도 그 값을 Return 할 수 없음
- 데이터 검색은 별도의 C# 메소드로 분리할 것





###	사용자 매핑

```mssql
USE [DBFirstDB]
GO
CREATE USER [coreuser] FOR LOGIN [coreuser]
GO
USE [DBFirstDB]
GO
ALTER ROLE [db_owner] ADD MEMBER [coreuser]
GO
```

