using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UiPathIsFunny.Model;

namespace UiPathIsFunny.Utility
{
    public static class ConfigValidate
    {

        // Validate Name
        public static Error NameValidate(string name, int indexEdit, List<Config> configs)
        {
            if (String.IsNullOrEmpty(name.Trim()))
            {
                return new Error
                {
                    IsError = true,
                    Message = "Please enter the name value"
                };
            }
            else
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
                {
                    return new Error
                    {
                        IsError = true,
                        Message = "The name is not valid :("
                    };
                }
                else
                {
                    if (indexEdit == -1)
                    {
                        // Add case:
                        if (configs.Where(_ => _.Name.Equals(name)).Count() > 0)
                        {
                            return new Error
                            {
                                IsError = true,
                                Message = "Opps! This name really exists(duplicate) in your config."
                            };
                        }
                        else
                        {
                            return new Error
                            {
                                IsError = false,
                                Message = String.Empty
                            };
                        }
                    }
                    else
                    {
                        // Edit case:
                        bool fl = false;
                        for (int i = 0; i < configs.Count(); i++)
                        {
                            if (i != indexEdit && configs[i].Name.Equals(name))
                            {
                                fl = true;
                                break;
                            }
                        }
                        if (fl)
                        {
                            return new Error
                            {
                                IsError = true,
                                Message = "Opps! This name really exists(duplicate) in your config."
                            };
                        }
                        else
                        {
                            return new Error
                            {
                                IsError = false,
                                Message = String.Empty
                            };
                        }
                    }

                }
            }
        }

        // Validate Keyword
        public static Error KeywordValidate(string keyword, int indexEdit, List<Config> configs)
        {
            if (String.IsNullOrEmpty(keyword.Trim()))
            {
                return new Error
                {
                    IsError = true,
                    Message = "Please enter the keyword value"
                };
            }
            else
            {
                string str = keyword.Replace(" ", String.Empty);
                if (str.Length <= 2)
                {
                    return new Error
                    {
                        IsError = true,
                        Message = "Opps! This keyword must containt more than 2 character"
                    };
                }
                else
                {
                    if (indexEdit == -1)
                    {
                        // add case:
                        if (configs.Where(_ => _.Keyword.Equals(keyword)).Count() > 0)
                        {
                            return new Error
                            {
                                IsError = true,
                                Message = "Opps! This keyword really exists(duplicate) in your config."
                            };
                        }
                        else
                        {
                            return new Error
                            {
                                IsError = false,
                                Message = String.Empty
                            };
                        }
                    }
                    else
                    {
                        bool fl = false;
                        for (int i = 0; i < configs.Count(); i++)
                        {
                            if (i != indexEdit && configs[i].Keyword.Equals(keyword))
                            {
                                fl = true;
                                break;
                            }
                        }
                        if (fl)
                        {
                            return new Error
                            {
                                IsError = true,
                                Message = "Opps! This keyword really exists(duplicate) in your config."
                            };
                        }
                        else
                        {
                            return new Error
                            {
                                IsError = false,
                                Message = String.Empty
                            };
                        }
                    }
                }
            }
        }

    }
}
