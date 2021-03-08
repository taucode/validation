using FluentValidation.Validators;
using System;
using System.Collections.Generic;

namespace TauCode.Validation
{
    public class CodeValidator : PropertyValidatorWithMessage
    {
        protected int MaxLength { get; }
        protected int MinLength { get; }
        protected HashSet<char> Alphabet { get; }
        protected HashSet<char> StartingChars { get; }
        protected HashSet<char> Separators { get; }

        public CodeValidator(
            int minLength,
            int maxLength,
            char[] alphabet,
            char[] startingChars,
            char[] separators,
            string message)
            : base(message)
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

            if (alphabet.Length == 0)
            {
                throw new ArgumentException($"'{nameof(alphabet)}' cannot be empty.", nameof(alphabet));
            }

            if (startingChars == null)
            {
                throw new ArgumentNullException(nameof(startingChars));
            }

            if (startingChars.Length == 0)
            {
                throw new ArgumentException($"'{nameof(startingChars)}' cannot be empty.", nameof(startingChars));
            }

            if (separators == null)
            {
                throw new ArgumentNullException(nameof(separators));
            }

            this.MinLength = minLength;
            this.MaxLength = maxLength;
            this.Alphabet = new HashSet<char>(alphabet);
            this.StartingChars = new HashSet<char>(startingChars);
            this.Separators = new HashSet<char>(separators);
        }

        public CodeValidator(
            int minLength,
            int maxLength,
            char[] alphabet,
            char[] startingChars,
            char[] separators)
            : this(
                minLength,
                maxLength,
                alphabet,
                startingChars,
                separators,
                "'{PropertyName}' must be a valid code with length {expectedLength}.")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var code = (string)context.PropertyValue;

            if (code == null)
            {
                return true; // code might be null, not our case
            }

            if (code == string.Empty)
            {
                return false; // code cannot be empty
            }

            var isValid = true;

            if (code.Length < this.MinLength || code.Length > this.MaxLength)
            {
                isValid = false;
            }
            else
            {
                var index = 0;
                var gotSeparator = false;

                while (true)
                {
                    if (index == code.Length)
                    {
                        isValid = !gotSeparator; // code cannot end with separator
                        break;
                    }

                    var c = code[index];

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
                var expectedLength = this.MinLength == this.MaxLength ? this.MinLength.ToString() : $"{this.MinLength}..{this.MaxLength}";

                context.MessageFormatter
                    .AppendArgument("expectedLength", expectedLength);
            }

            return isValid;
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
    }
}
