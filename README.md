#### RUN THE FOLLOWING QUERRY ON MYSQL ADMIN PANEL ####

```Sql
-- Create database named 'mydb'
CREATE DATABASE IF NOT EXISTS mydb;

-- Use database 'mydb'
USE mydb;

-- Create table for Members
CREATE TABLE Members (
    MemberId VARCHAR(20) NOT NULL PRIMARY KEY,
    MemberName VARCHAR(20) DEFAULT NULL,
    MemberDateOfBirth DATETIME DEFAULT NULL,
    MemberEmail VARCHAR(50) DEFAULT NULL,
    MemberPhone VARCHAR(10) DEFAULT NULL
);

-- Create table for Events
CREATE TABLE Events (
    EventId VARCHAR(20) NOT NULL PRIMARY KEY,
    EventName VARCHAR(30) DEFAULT NULL,
    EventDescription VARCHAR(50) DEFAULT NULL,
    EventDateTime DATETIME DEFAULT NULL,
    EventVenue VARCHAR(20) DEFAULT NULL
);

-- Create table for Events-to-Members
CREATE TABLE EventMembers (
    EventId VARCHAR(20) NOT NULL,
    MemberId VARCHAR(20) NOT NULL,
);

-- Add data to 'Events' table
INSERT INTO `events`(`EventId`, `EventName`, `EventDescription`, `EventDateTime`, `EventVenue`) VALUES 
('e001','Policy Revision Submit','Discuss Company Policies','2025-02-28 19:30:00','Soaltee Hotel'),
('e002','Company Annual Party','Annual party for all company employees','2025-03-10 12:00:00','Hotel Radisson'),
('e003','Market Fair Event','Fair event for individual company partners','2025-02-25 13:30:00','Hotel Annapurna'),
('e004','Market Progressions','Discuss company progress','2025-03-03 15:00:00','Shangri-La Hotel'),
('e005','Trade Elevate Summit','Discuss future for the company','2025-02-14 10:00:00','Hotel The Kingsbury'),
('e006','Business Connect Fest','Fest event for company partners','2025-02-20 13:30:00','Hotel Pokhara Grande'),
('e007','Radiant Rendezvous','Secret higher-ups meeting','2025-02-09 19:00:00','The Fulbari Resort'),
('e008','Tech Sphere Event','event for technological show-off','2025-03-15 09:45:00','The Hotel Malla'),
('e009','Profit Analysis','Discuss company progress','2025-03-28 19:00:00','The Hotel Everest'),
('e010','Tech Insights Forum','Discuss company technological advancements','2025-02-12 11:00:00','Hotel Aloft');

-- Add data to 'Members' table
INSERT INTO `members`(`MemberId`, `MemberName`, `MemberDateOfBirth`, `MemberEmail`, `MemberPhone`) VALUES 
('m001', 'Anusha Sherstha', '1986-01-31 12:36:10', 'anusha078@academiacollege.edu.np', '2257714375'),
('m002', 'Nafisha Maharjan', '1982-12-13 02:38:15', 'nafisha078@academiacollege.edu.np', '9076787754'),
('m003', 'Sadik Karki', '2001-10-19 06:57:46', 'sadikkarki10@gmail.com', '9548440017'),
('m004', 'Beck Tregenna', '1992-05-09 06:14:34', 'btenna3@ocn.ne.jp', '8716616923'),
('m005', 'Kaleb Stennes', '1997-11-16 10:33:46', 'kstennes4@umn.edu', '8353081536')	,
('m006', 'Deina Arkley', '1994-01-31 01:57:30', 'darkley5@phoca.cz', '2769949356'),
('m007', 'Reeba Chedgey', '1984-07-22 22:56:19', 'rchedgey6@blog.com', '9112899364')	,
('m008', 'Annice Greenhowe', '1987-12-28 07:07:03', 'greenhoe7@mlb.com', '2339056898'),
('m009', 'Natala Whelband', '1998-06-19 15:41:29', 'nwhelband8@usgs.gov', '9128527561'),
('m010', 'Marv Kestell', '1996-07-30 13:48:24', 'mkestell9@bloom.com', '4799451262');

-- Add data to 'Events-to-Members' table
INSERT INTO `eventattendees`(`EventId`, `MemberId`) VALUES 
('e005', 'm001'),
('e005', 'm006'),
('e005', 'm010'),
('e005', 'm008'),
('e008', 'm008'),
('e008', 'm007'),
('e008', 'm008'),
('e004', 'm003'),
('e004', 'm007'),
('e004', 'm004');
