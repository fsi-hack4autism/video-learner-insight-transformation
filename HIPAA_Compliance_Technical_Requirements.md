# HIPAA Compliance Technical Requirements

The Security Rule Technical Safeguards are the technology and related policies and procedures
that protect EPHI and control access to it. The Technical Safeguards standards apply to all
EPHI. The Rule requires a covered entity to comply with the Technical Safeguards standards
and provides the flexibility to covered entities to determine which technical security measures
will be implemented. For more information visit https://www.hhs.gov/sites/default/files/ocr/privacy/hipaa/administrative/securityrule/techsafeguards.pdf

## 1. Authentication
- System should be able to verify that a person or entity seeking access to electronic protected health information is the one claimed
- Define the method of authentication to be used: single vs multi factor authentication
- How will the system be able to authenticate users upon log in.
- Will there need to be different authentications for different user profiles?

## 2. Access Control

### a. Unique User Identification (Required)
- Each user should be assigned a unique name and/or number for identifying and tracking user identity on the application
- Define field for uniquness
- Define formart for the field
- Define how metrics of the field will be tracked

### b. Emergency Access Procedure (Required)
- System should have defined procedures for obtaining necessary electronic protected health information during an emergency. This should include details about who can have emergency access and what the proceedure are for getting this access

### c. Automatic Logoff (Optional)
- System should have electronic procedures that terminate an electronic session
after a predetermined time of inactivity.
- Define how much idle time is takes before system automatically logs off the user
- Define which devices that automatic log off is enabled for. Is it all or some?
- Define if warning message is needed before user is logged off
- Define how idle time will be tracked

### d. Encryption and Decryption (Optional)
- Syetm should have a mechanism to encrypt and decrypt electronic protected
health information.
- Define which EPHI need to be encrypted
- Define approproiate encryption mechanism to protect EPHI from being accessed by unathorized entities

## 3. Audit Controls
- System should have mechanisms that record and examine activity that contain or use electronic protected health information.
- Define reasonable audit mechanisms that the system can support

## 4. Integrity
- System should have ability to protect electronic protected health information from improper alteration or destruction.
- System should have ability to restore EPHI in the event of deletion or alteration
- All changes to data should be tracked and recorded with time stamp and the individual/system that's making the change

## 5. Transmission Security
- System should have security measures to guard against unauthorized access to electronic protected health information that is being transmitted over an electronic communications network.
- System should have security measures to ensure that electronically transmitted electronic protected health information is not improperly modified without detection until disposed of
