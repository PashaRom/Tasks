Feature:Cars
	In order to avoid silly mistakes
	As a averange user
	I want to check equal data from search form

@positive
Scenario Outline: Check cars data from search form 
	Given I have launch wep-application by 'https://www.cars.com/'
	When I click on the top menu link 'Research'
	Then Research page is opened

	When I search car brand '<FirstCarBrand>' model '<FirstCarModel>' and production year '<FirstCarYear>'
	Then Fields search form have values brand '<FirstCarBrand>' model '<FirstCarModel>' production year '<FirstCarYear>'
		And The description of car page is opened
	
	When I click on link CompareTrim
	Then Target options of car page is opened with brand '<FirstCarBrand>' model '<FirstCarModel>' production year '<FirstCarYear>'
	
	When I save options of car: brand '<FirstCarBrand>', model '<FirstCarModel>', production year, '<FirstCarYear>' available engines, transmissions
		And I am back to Home page
	Then Home page is opened
	
	When I click on the top menu link 'Research'
	Then Research page is opened
	
	When I search car brand '<SecondCarBrand>' model '<SecondCarModel>' and production year '<SecondCarYear>'
	Then Fields search form have values brand '<SecondCarBrand>' model '<SecondCarModel>' production year '<SecondCarYear>'
		And The description of car page is opened
	
	When I click on link CompareTrim
	Then Target options of car page is opened with brand '<SecondCarBrand>' model '<SecondCarModel>' production year '<SecondCarYear>'
	
	When I save options of car: brand '<SecondCarBrand>', model '<SecondCarModel>', production year, '<SecondCarYear>' available engines, transmissions	
		And I click on the top menu link 'Research' on Trims page
	Then Research page is opened
	
	When I click on the Side-by-side Comparisons block
	Then Compare page is opened
	
	When Choose first car for comparing: brand '<FirstCarBrand>', model '<FirstCarModel>', production year, '<FirstCarYear>'
	Then Chosen car page is opened
	
	When I use Add button to add for comparison secomd car: brand '<SecondCarBrand>', model '<SecondCarModel>', production year '<SecondCarYear>'	
	Then Models have been successfully selected for comparison 
	
	When Check Compare page for both cars
	Then Options of cars on Compare page are equal with options of cars have got previous steps: '<FirstCarBrand>', '<FirstCarModel>', '<FirstCarYear>', '<SecondCarBrand>', '<SecondCarModel>', '<SecondCarYear>'
	
	Examples: 
	| FirstCarBrand | FirstCarModel | FirstCarYear | SecondCarBrand | SecondCarModel | SecondCarYear |
	| Nissan        | Murano        | 2020         | Kia            | Rio            | 2020          |
	
