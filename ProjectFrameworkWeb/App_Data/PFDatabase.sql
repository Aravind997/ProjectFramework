
#
# Table structure for table 'settings_tb'
#

DROP TABLE IF EXISTS `settings_tb`;

CREATE TABLE `settings_tb` (
  `ID` INTEGER NOT NULL AUTO_INCREMENT, 
  `SettingsKey` VARCHAR(50), 
  `SettingsValue` LONGTEXT, 
  `Reserved1` LONGTEXT, 
  INDEX (`SettingsKey`), 
  PRIMARY KEY (`ID`)
) ENGINE=myisam DEFAULT CHARSET=utf8;

SET autocommit=1;

#
# Dumping data for table 'settings_tb'
#

INSERT INTO `settings_tb` (`ID`, `SettingsKey`, `SettingsValue`, `Reserved1`) VALUES (1, 'MainHeading', 'ASP.NET Project Framework', NULL);
INSERT INTO `settings_tb` (`ID`, `SettingsKey`, `SettingsValue`, `Reserved1`) VALUES (2, 'MainDesc', 'ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript. This information is taken from settings_tb , MainDesc Key.', NULL);
INSERT INTO `settings_tb` (`ID`, `SettingsKey`, `SettingsValue`, `Reserved1`) VALUES (3, 'AppName', 'App Name ', NULL);
INSERT INTO `settings_tb` (`ID`, `SettingsKey`, `SettingsValue`, `Reserved1`) VALUES (4, 'EnableMobAuth', 'False', NULL);
# 4 records

#
# Table structure for table 'user_info_tb'
#

DROP TABLE IF EXISTS `user_info_tb`;

CREATE TABLE `user_info_tb` (
  `ID` INTEGER NOT NULL AUTO_INCREMENT, 
  `FirstName` VARCHAR(50), 
  `LastName` VARCHAR(50), 
  `UserName` VARCHAR(100), 
  `Email` VARCHAR(100), 
  `PhoneNo` VARCHAR(50), 
  `Address` LONGTEXT, 
  `Password` VARCHAR(50), 
  `Role` VARCHAR(15), 
  `OrgID` INTEGER DEFAULT 0, 
  `AdditionalUserInfo` LONGTEXT, 
  `Reserved1` LONGTEXT, 
  INDEX (`OrgID`), 
  PRIMARY KEY (`ID`)
) ENGINE=myisam DEFAULT CHARSET=utf8;

SET autocommit=1;

#
# Dumping data for table 'user_info_tb'
#

INSERT INTO `user_info_tb` (`ID`, `FirstName`, `LastName`, `UserName`, `Email`, `PhoneNo`, `Address`, `Password`, `Role`, `OrgID`, `AdditionalUserInfo`, `Reserved1`) VALUES (1, 'Site', 'Admin', 'admin', 'admin@yourcompany.com', '9853536855', 'Pala | Kerala | India', 'aLbQNeqrkDA=', 'Admin', NULL, NULL, NULL);
# 1 records

