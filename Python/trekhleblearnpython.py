# github.com/trekhleb/learn-python:
# 1.Operators
def test_arithmetic_operators():

    # Addition
    assert 5 + 3 == 8

    # subtraction
    assert 5 - 3 == 2

    # Multiplication
    assert 5 * 3 == 15
    assert isinstance(5 * 3, int)

    # Division
    # Result of division is float number
    assert 5 / 3 == 1.6666666666666667
    assert 8 / 4 == 2
    assert isinstance(5 / 3, float)
    assert isinstance(8 / 4, float)

    # Modules
    assert 5 % 3 == 2

    # Exponentiation
    assert 5 ** 3 == 125
    assert 2 ** 3 == 8
    assert 2 ** 4 == 16
    assert 2 ** 5 == 32
    assert isinstance(5 ** 3, int)

    # Floor division
    assert 5 // 3 == 1
    assert 6 // 3 == 2
    assert 7 // 3 == 2
    assert 9 // 3 == 3
    assert isinstance(5 // 3, int)

def test_bitwise_operators():
    # AND
    # Sets each bit to 1 if both bits are 1.
    # Example:
    # 5 = 0b0101
    # 3 = 0b0011
    assert 5 & 3 == 1 # 0b0001

    # OR
    # Sets each bit to 1 if one of two bits is 1.
    # Example:
    # 5 = 0b0101
    # 3 = 0b0011
    assert 5 | 3 == 7 # 0b0111

    # NOT
    # Inverts all the bits.
    # 5  = 0b0101
    # ~5 = 1b1010
    assert ~5 == -6

    # XOR
    # Sets each bit to 1 if only one of two bits is 1
    # Example
    # 5 = 0b0101
    # 3 = 0b0011
    #3^5= 0b0110 = 6
    number = 5
    number ^= 3 # number = number ^ 3
    assert 5 ^ 3 == 6

    # Signed right shift 有符号位右移：每右移一位就相当于除2
    # shift right by pushing copies of the leftmost bit in from the left and let the rightmost 
    # bits fall off
    # Example:
    # 5 = 0b0101
    assert 5 >> 1 == 2 # 0b0010
    assert 5 >> 2 == 1 # 0b0001
    assert 6 >> 1 == 3 # 0b0011

    # Zero fill left shift 高位补0左移：每左移一位就相当于乘2
    # Shift left by pushing zeros in from the right and let the leftmost bits fall off.
    # Example:
    # 5 = 0b0101
    assert 5 << 1 == 10 # 0b1010
    assert 5 << 2 == 20 # 0b1_0100
    assert 6 << 1 == 12 # 0b1100

def test_assignment_operator():
    # Assignment: =
    number = 5
    assert number == 5

    # Multiple assignment
    # The variables first_variable and second_variable simultanenously get the new values 0 and 1
    first_variable, second_variable = 0, 1
    assert first_variable == 0
    assert second_variable == 1

    # You may even switch variable values using multiple assignment
    first_variable, second_variable = second_variable, first_variable
    assert first_variable == 1
    assert second_variable == 0

def test_augmented_assignment_operators():
    """ Assignment operator combined with arithmetic and bitwise operators """

    # Assignment: +=
    number = 5
    number += 3
    assert number == 8

    # Assignment: -=
    number = 5
    number -= 3
    assert number == 2

    # Assignment: *=
    number = 5
    number *= 3
    assert number == 15

    # Assignment: /=
    number = 8
    number /= 4
    assert number == 2

    # Assignment: %=
    number = 8
    number %= 3
    assert number == 2
    number = 5
    number %= 3
    assert number == 2

    # Assignment: //=
    number = 5
    number //= 3
    assert number == 1

    # Assignment: **=
    number = 5
    number **= 3
    assert number == 125 # 5^3 = 5 * 5 * 5 = 125

    # Assignment: &= 
    number = 5         # 5 = 0b0101
    number &= 3        # 3 = 0b0011
    assert number == 1 # 1 = 0b0001

    # Assignment: |=
    number = 5         # 5 = 0b0101
    number |= 3        # 3 = 0b0011
    assert number == 7 # 7 = 0b0111

    # Assignment: ^=
    number = 5          # 5 = 0b0101
    number ^= 3         # 3 = 0b0011
    assert number == 6  # 6 = 0b0110

    # Assignment: >>=
    number = 5
    number >>= 3        # (((5 // 2) // 2) // 2 )
    assert number == 0 
    numbe1r = 5
    numbe1r >>= 2
    assert numbe1r == 1

    # Assignment: <<=
    number = 5
    number <<= 3        # 5 * 2 * 2 * 2
    assert number == 40 

def test_comparison_operators():
    # Equal
    number = 5
    assert number == 5

    # Not equal
    number = 5
    assert number != 3

    # Greater than
    number = 5
    assert number > 3

    # Less than
    number = 5
    assert number < 8

    # Greater than or equal to
    number = 5
    assert number >= 5
    assert number >= 4

    # Less than or equal to
    number = 5
    assert number <= 5
    assert number <= 6

def test_logical_operators():
    # Let's work with these number to illustrate logic operators
    first_number = 5
    second_number = 10

    # and 
    # Returns True if both statements are true
    assert first_number > 0 and second_number < 20

    # or
    # Returns True if one of the statements is true
    assert first_number > 5 or second_number < 20

    # not
    # Reverse the result, returns False if the result is true
    # pylint: disable=unneeded-not
    assert not first_number == second_number
    assert first_number != second_number

def test_identity_operators():
    # Let's illustrate identity operators based on the following lists
    first_fruits_list = ["apple", "banana"]
    second_fruits_list = ["apple", "banana"]
    third_fruits_list = first_fruits_list

    # is
    # Returns true if both variables are the same object
    # Exapmle
    # first_fruits_list and third_fruits_list are the same object
    assert first_fruits_list is third_fruits_list

    # is not
    # Returns true if both variables are not the same object
    # Example
    # first_fruits_list and second_fruits_list are not the same objects, even if they have 
    # the same content
    assert first_fruits_list is not second_fruits_list

    # To demonstrate the difference between "is" and "==": this comparison returns True because
    # first_fruits_list is equal to second_fruits_list
    assert first_fruits_list == second_fruits_list

def test_membership_operators():
    # Let's use the following fruit list to illustrate membership concept
    fruit_list = ["apple", "banana"]

    # in
    # Returns True if a sequence with the specified value is present in the object
    # Returns True because a sequence with the value "banana" is in the list
    assert "banana" in fruit_list

    # not in
    # Returns True if a sequence with the specified value is not present in the object
    # Returns True because a sequence with the value "pineapple" is not in the list
    assert "pineapple" not in fruit_list

# 2.Data Types
def test_datatypes():
    """ Integer type
    int, or integer, is a whole number, positive or negative
    without decimals, of unlimited length """

    positive_integer = 1
    negative_integer = -3255522
    big_integer = 35656222554887711
    assert isinstance(positive_integer, int)
    assert isinstance(negative_integer, int)
    assert isinstance(big_integer, int)
    
    """ Boolean
    Booleans represent the truth values False and True. The two objects representing the values
    False and True are the only Boolean objects. The Boolean type is a subject of the integer type,
    and Boolean values behave like the values 0 and 1, respectively, in almost all contexts, 
    the exception being that when converted to a string, the strings "False" or "True" are returned,
    respectively. """

    true_boolean = True
    false_boolean = False
    assert true_boolean
    assert not false_boolean
    assert isinstance(true_boolean, bool)
    assert isinstance(false_boolean, bool)
    # Let's try to cast boolean to string
    assert str(true_boolean) == "True"
    assert str(false_boolean) == "False"

    """ Float type
    Float, or "floating point number" is a number, positive or negative,
    containing one or more decimals """
    float_number = 7.0
    # Another way of declaring float is using float() function
    float_number_via_function = float(7)
    float_negative = -35.59
    assert float_number == float_number_via_function
    assert isinstance(float_number, float)
    assert isinstance(float_number_via_function, float)
    assert isinstance(float_negative, float)
    # Float can also be scientific numbers with an "e" to indicate
    # the power of 10
    float_with_small_e = 35e3
    float_with_big_e = 12E4
    assert float_with_small_e == 35000
    assert float_with_big_e == 120000
    assert isinstance(12E4, float)
    assert isinstance(-87.7e100, float)

    """ Complex Type """
    complex_number_1 = 5 + 6j
    complex_number_2 = 3 - 2j
    assert isinstance(complex_number_1, complex)
    assert isinstance(complex_number_2, complex)
    assert complex_number_1 * complex_number_2 == 27 + 8j

def test_number_operators():
    """ Basic operations """
    # Addition
    assert 2 + 4 == 6

    # Multiplication
    assert 2 * 4 == 8

    # Division always returns a floating point number
    assert 12 / 3 == 4.0
    assert 12 / 5 == 2.4
    assert 17 / 3 == 5.666666666666667

    # Modulo operator returns the remainder of division
    assert 12 % 3 == 0
    assert 13 % 3 == 1

    # Floor division discards the fractional part
    assert 17 // 3 == 5

    # Raising the number to specific power
    assert 5 ** 2 == 25 # 5 squared 
    assert 2 ** 7 == 128 # 2 to the power of 7

    # There is full support for floating point; operators with
    # mixed type operands convert the integer operand to floaing point
    assert 4 * 3.75 - 1 == 14.0

import pytest

def test_string_type():
    # String with double quotes
    name_1 = "John"
    # String with single quotes
    name_2 = 'John'
    # String created with different kind of quotes are treated the same
    assert name_1 == name_2
    assert isinstance(name_1, str)
    assert isinstance(name_2, str)

    # \ can be used to escape quotes
    # use \' to escape the single quote or use double quotes instead
    single_quote_string = 'doesn\'t'
    double_quote_string = "doesn't"
    assert single_quote_string == double_quote_string

    # \n means newline
    multiline_string = 'First line.\nSecond line.'
    # Without print(), \n is included in the output
    # But with print(), \n produces a new line
    assert multiline_string == 'First line.\nSecond line.'

    # Strings can be indexted, with the first character having index 0
    # There is no separate character type; a character is simply a string
    # of size one. Note that since -0 is the same as 0, negative indices
    # start from -1
    word = 'Python'
    assert word[0] == 'P'   # First character
    assert word[5] == 'n'   # Fifth character
    assert word[-1] == 'n'  # Last character
    assert word[-2] == 'o'  # Second-last character
    assert word[-6] == 'P'  # Sixth from the ene or zeroth from the begining
    assert isinstance(word[0], str)

    # In addition to indexing, slicing is also supported. While indexing is
    # used to obtain individual characters, slicing allows you to obtain
    # substring:
    assert word[0:2] == 'Py' # Characters from position 0 (included) to 2(excluded)
    assert word[2:5] == 'tho' # Characters from position 2 (included) to 5(excluded)

    # Note how the start is always included, and the end always excluded
    # This makes sure that s[:i] + s[i:] is always equal to s:
    assert word[:2] + word[2:] == 'Python'
    assert word[:4] + word[4:] == 'Python'

    # Slice indices have useful defaults; an omitted first index defaults to
    # zero, an omitted second index defaults to the size of the string being
    # sliced
    assert word[:2] == 'Py'
    assert word[4:] == 'on'
    assert word[-2:] == 'on'
    # One way to remember how slices work is to think of the indices as
    # pointing between characters, with the left edge of the first character
    # number 0. Then the right edge of the last character of a string of n
    # characters has index n, for example:
    # +---+---+---+---+---+---+
    # | P | y | t | h | o | n |
    # +---+---+---+---+---+---+
    # 0   1   2   3   4   5   6
    #-6  -5  -4  -3  -2  -1  -0

    # Attemping to use an index that is too large will result in an error
    with pytest.raises(Exception):
        not_existing_character = word[42]
        assert not not_existing_character

    # However, out of range slice indexes are handled gracefully when used
    # for slicing:
    assert word[4:42] == 'on'
    assert word[42:] == ''

    # Python strings cannot be changed - they are immutable. Therefore,
    # assigning to an indexed position in the string
    # result in an error:
    with pytest.raises(Exception):
        # pylint: disable=unsupported-assignment-operation
        word[0] = 'J'

    # If you need a different string, you should create a new one:
    assert 'J' + word[1:] == 'Jython'
    assert word[:2] + 'py' == 'Pypy'

    # The built-in function len() returns the lenght of a string:
    characters = 'supercalifragilisticexpialidocious'
    assert len(characters) == 34

    # String literals can span multiple lines. One way is using triple-quotes: """..."""
    # or '''...'''. End of lines are automaticallt included in the string, but it's possible 
    # to prevent this by adding a \ at the end of the line. The following example:
    multi_line_string = '''\
        First line
        Second line
    '''
    
    assert multi_line_string == '''\
        First line
        Second line
    '''

def test_string_operators():
    """Basic operations

    Strings can be concatenated (glued together) with the + operator,
    and repeated with *: 3 times 'un', followed by 'ium'
    """

    assert 3 * 'un' + 'ium' == 'unununium'

    # 'Py' 'thon'
    python = 'Py' 'thon'
    assert python == 'Python'

    # This features is particularly useful when you want to break long strings:
    text = (
        'Put several strings within parentheses '
        'to have them joined together.'
    )
    assert text == 'Put several strings within parentheses to have them joined together.'

    # If you want to concatenate variables or a variable and a literal, use +:
    prefix = 'Py'
    assert prefix + 'thon' == 'Python'








if __name__ == "__main__":
    # test_arithmetic_operators()
    # test_bitwise_operators()
    # test_augmented_assignment_operators()
    # test_comparison_operators()
    # test_logical_operators()
    # test_identity_operators()
    # test_membership_operators()
    # test_datatypes()
    # test_number_operators()
    test_string_operators()
    print("nothing wrong")









# github.com/TheAlgorithms/Python
from typing import List

def bucket_sort(my_list: list) -> list:
    if len(my_list) == 0:
        return []
    min_value, max_value = min(my_list), max(my_list)
    bucket_count = int(max_value - min_value) + 1
    buckets: List[list] = [[] for _ in range(bucket_count)]

    for i in range(len(my_list)):
        buckets[(int(my_list[i] - min_value) // bucket_count)].append(my_list[i])

    return [v for bucket in buckets for v in sorted(bucket)]

def double_sort(lst):
    no_of_elements = len(lst)
    for i in range(
        0,int(((no_of_elements - 1) / 2) + 1)
    ):
        for j in range(0,no_of_elements - 1):
            if( lst[j + 1] < lst[j]):
                temp = lst[j + 1]
                lst[j + 1] = lst[j]
                lst[j] = temp
            if( lst[no_of_elements - 1 - j] < lst[no_of_elements - 2 - j]):
                temp = lst[no_of_elements - 1 - j]
                lst[no_of_elements - 1 - j] = lst[no_of_elements - 2 - j]
                lst[no_of_elements - 2 - j] = temp
    return lst