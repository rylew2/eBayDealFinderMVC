# eBayDealFinderMVC

## Overview

This application pulls deals from a Deal of the Day XML feed and compares their price to an average eBay completed items price to see if a price arbitrage exists. 
Currently only one Deal of the Day rss feed is compared against eBay prices although remnants of other deal data is still left in the program from when those sites were up. 

## Usage
To see the price comparison, the program needs a breakpoint at the end of the compareDealsToEbay() function and a watch on the anotherdd variable. 

## API and External References
  The eBay Finding API is used in this program. A service reference to the following is included:
  http://developer.ebay.com/webservices/Finding/latest/FindingService.wsdl
  
  External RSS Deal of the Day feed: http://feeds.feedburner.com/Dealsucker?format=xml

## Issues and Planned Features
- Since this codebase was migrated from a WinForms application, much of the code is misplaced and needs to be refactored (ie.. eBay API data manipulated in the controller file, uneeded Deal of the Day feeds used) 
- Integrate other deal of the day services - currently only one rss feed is being used. The application was originally using multiple feeds. 
- Distinguish condition of deal of the day offers (ie refurbished, used, etc..) 
- Set up controller tests
- Setup Azure/AppHarbor hosting 




