using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;

namespace TauCode.Validation.Codes
{
    public class CodeValidator<T> : PropertyValidator<T, string>
    {
        protected int MaxLength { get; }
        protected int MinLength { get; }

        protected HashSet<char> Alphabet { get; }
        protected HashSet<char> StartingChars { get; }
        protected HashSet<char> Separators { get; }

        public CodeValidator(
            int minLength,
            int maxLength,
            HashSet<char> alphabet,
            HashSet<char> startingChars,
            HashSet<char> separators)
        {
            if (minLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minLength));
            }

            if (maxLength < minLength)
            {
                throw new ArgumentException($"'{nameof(maxLength)}' must be not less than '{nameof(minLength)}'", nameof(maxLength));
            }

            if (alphabet == null)
            {
                throw new ArgumentNullException(nameof(alphabet));
            }

            if (alphabet.Count == 0)
            {
                throw new ArgumentException($"'{nameof(alphabet)}' cannot be empty.", nameof(alphabet));
            }

            if (startingChars == null)
            {
                throw new ArgumentNullException(nameof(startingChars));
            }

            if (startingChars.Count == 0)
            {
                throw new ArgumentException($"'{nameof(startingChars)}' cannot be empty.", nameof(startingChars));
            }

            if (separators == null)
            {
                throw new ArgumentNullException(nameof(separators));
            }

            this.MinLength = minLength;
            this.MaxLength = maxLength;

            this.Alphabet = alphabet;
            this.StartingChars = startingChars;
            this.Separators = separators;
        }

        protected bool IsAlphabetChar(char c)
        {
            return this.Alphabet.Contains(c);
        }

        protected bool IsSeparator(char c)
        {
            return this.Separators.Contains(c);
        }

        protected bool IsStartingChar(char c)
        {
            return this.StartingChars.Contains(c);
        }

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value == null)
            {
                return false;
            }

            if (value == string.Empty)
            {
                return false; // code cannot be empty
            }

            bool isValid;

            if (value.Length < this.MinLength || value.Length > this.MaxLength)
            {
                isValid = false;
            }
            else
            {
                var index = 0;
                var gotSeparator = false;

                while (true)
                {
                    if (index == value.Length)
                    {
                        isValid = !gotSeparator; // code cannot end with separator
                        break;
                    }

                    var c = value[index];

                    // 0-th char check
                    if (index == 0)
                    {
                        if (!this.IsStartingChar(c))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    else
                    {
                        // c is separator?
                        if (this.IsSeparator(c))
                        {
                            if (gotSeparator)
                            {
                                isValid = false; // cannot have two consequent separators
                                break;
                            }

                            gotSeparator = true;
                        }
                        else
                        {
                            // not separator.

                            if (!this.IsAlphabetChar(c))
                            {
                                isValid = false;
                                break;
                            }

                            gotSeparator = false;
                        }
                    }

                    index++;
                }
            }

            if (!isValid)
            {
                var expectedLengthDescription = this.MinLength == this.MaxLength
                    ? this.MinLength.ToString()
                    : $"{this.MinLength}..{this.MaxLength}";

                context
                    .MessageFormatter
                    .AppendArgument("expectedLengthDescription", expectedLengthDescription);
            }

            return isValid;
        }

        public override string Name => "CodeValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "'{PropertyName}' must be a valid code with length {expectedLengthDescription}.";
        }
    }
}
