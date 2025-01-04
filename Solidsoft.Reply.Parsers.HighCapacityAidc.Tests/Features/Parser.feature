Feature: Parser

A short summary of the feature

Scenario Outline: Parse valid GS1 element strings - 3 AIs
    Given I have a GS1 element string "<elementString>"
    When I extract AIs and values
    Then the result should contain:
      | AI    | Value    |
      | <AI1> | <Value1> |
      | <AI2> | <Value2> |
      | <AI3> | <Value3> |

    Examples:
      | elementString                        | AI1  | Value1         | AI2  | Value2         | AI3  | Value3 | AI4 | Value4 |
      | 010541234500001310ABC123<GS>17290331 | 01   | 05412345000013 | 10   | ABC123         | 17   | 290331 |     |        |
      | 31030001890105412345000013<GS>10XYZ  | 3103 | 000189         | 01   | 05412345000013 | 10   | XYZ    |     |        |
      | 800304965031954585<GS>17290331<GS>10XYZ  | 8003 | 04965031954585 | 17   | 290331         | 10   | XYZ    |     |        |

Scenario Outline: Parse valid GS1 element strings - 1 AI
    Given I have a GS1 element string "<elementString>"
    When I extract AIs and values
    Then the result should contain:
      | AI    | Value    |
      | <AI1> | <Value1> |

    Examples:
      | elementString      | AI1  | Value1         | 
      | 800304965031954585 | 8003 | 04965031954585 | 

Scenario Outline: Parse valid GS1 element strings - 4 AIs
    Given I have a GS1 element string "<elementString>"
    When I extract AIs and values
    Then the result should contain:
      | AI    | Value    |
      | <AI1> | <Value1> |
      | <AI2> | <Value2> |
      | <AI3> | <Value3> |
      | <AI4> | <Value4> |

    Examples:
      | elementString                                  | AI1  | Value1         | AI2  | Value2 | AI3  | Value3 | AI4 | Value4 |
      | 0105412345000013310300018939232172<GS>10ABC123 | 01   | 05412345000013 | 3103 | 000189 | 3923 | 2172   | 10  | ABC123 |


Scenario Outline: Parse valid MH10.8.2 element strings - 3 DIs
    Given I have an MH10.8.2 element string "<elementString>"
    When I extract DIs and values
    Then the MH10.8.2 result should contain:
      | DI    | Value    |
      | <DI1> | <Value1> |
      | <DI2> | <Value2> |
      | <DI3> | <Value3> |

    Examples:
      | elementString                                               | DI1 | Value1       | DI2 | Value2 | DI3 | Value3 |
      | [)><RS>06<GS>9N112097776020<GS>1TABC123<GS>D290331<RS><EOT> | 9N  | 112097776020 | 1T  | ABC123 | D   | 290331 |

Scenario Outline: Parse valid MH10.8.2 element strings - 4 DIs
    Given I have an MH10.8.2 element string "<elementString>"
    When I extract DIs and values
    Then the MH10.8.2 result should contain:
      | DI    | Value    |
      | <DI1> | <Value1> |
      | <DI2> | <Value2> |
      | <DI3> | <Value3> |
      | <DI4> | <Value4> |

    Examples:
	  | elementString                                                                | DI1 | Value1       | DI2 | Value2       | DI3 | Value3 | DI4 | Value4 |
      | [)><RS>06<GS>9N112097776020<GS>S496320471563<GS>1TABC123<GS>D290331<RS><EOT> | 9N  | 112097776020 | S   | 496320471563 | 1T  | ABC123 | D   | 290331 |


Scenario Outline: Parse valid EAN element strings
    Given I have an EAN element string "<elementString>"
    When I extract the product code value
    Then the EAN result should contain:
      | AI    | Value    |
      | <AI1> | <Value1> |

    Examples:
	  | elementString       | AI1 | Value1         |
      | 5412345000013       | 01  | 05412345000013 |
      | 541234500015        | 01  | 00541234500015 |
      | 541234500001301     | 01  | 05412345000013 |
      | 541234500001300995  | 01  | 05412345000013 |

Scenario Outline: Parse valid ITF14 element strings
    Given I have an ITF14 element string "<elementString>"
    When I extract the product code value
    Then the ITF14 result should contain:
      | AI    | Value    |
      | <AI1> | <Value1> |

    Examples:
	  | elementString | AI1 | Value1          |
      | 05412345000013 | 01  | 05412345000013 |

Scenario Outline: Parse invalid GS1 element strings - 3 AIs
    Given I have a GS1 element string "<elementString>"
    When I extract AIs and values
    Then the result should contain:
      | AI    | Value    |
      | <AI1> | <Value1> |
      | <AI2> | <Value2> |
      | <AI3> | <Value3> |

    Examples:
      | elementString                         | AI1 | Value1         | AI2 | Value2  | AI3 | Value3 | AI4 | Value4 |
      | 010541234500001510ABC123<GS>17290331  | 01  | 05412345000015 | 10  | ABC123  | 17  | 290331 |     |        |
      | 010541234500001310ABC£123<GS>17290331 | 01  | 05412345000013 | 10  | ABC£123 | 17  | 290331 |     |        |
      | 010541234500001310ABC123<GS>17294295  | 01  | 05412345000013 | 10  | ABC123  | 17  | 294295 |     |        |

Scenario Outline: Parse invalid GS1 element string
    Given I have a GS1 element string "17290366"
    When I extract AIs and values
    Then the following exception should be included: "The value 290366 does not match the specified pattern for the data element."


