﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Semantic;

namespace GCL.Syntax.Data
{
    public class Production
    {
        private readonly HashSet<Symbol> productSet;
        private readonly int hashCode;

        public Symbol Producer { get; }

        public List<Symbol> Product { get; }

        public Production(Symbol producer, params Symbol[] extraProductSymbols)
        {
            if (producer.Type != SymbolType.NonTerminal)
                throw new GrammaticException("Producer cannot be of type Terminal or Epsilon.", producer);
            this.Producer = producer;
            Product = new List<Symbol>(extraProductSymbols);
            productSet = new HashSet<Symbol>(Product);
            hashCode = Producer.GetHashCode() + Product.Sum(symbol => symbol.GetHashCode()); 
        }

        public Production(Symbol producer, IEnumerable<Symbol> productionSymbols) : this(producer, productionSymbols.ToArray())
        {
            
        }

        public bool Produces(Symbol symbol)
        {
            return productSet.Contains(symbol);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"{Producer} -> ");
            foreach (var symbol in Product)
            {
                builder.Append(symbol);
            }
            return builder.ToString();
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || (obj is Production) == false)
                return false;
            var otherProduction = (Production)obj;
            if (otherProduction.Product.Count != Product.Count)
                return false;
            for (var i = 0; i < Product.Count; i++)
            {
                if (Product[i] != otherProduction.Product[i])
                    return false;
            }

            return true;
        }

        public static bool operator ==(Production p1, Production p2)
        {
            if ((object) p1 == null || (object) p2 == null)
                return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Production p1, Production p2)
        {
            return !(p1 == p2);
        }
    }
}