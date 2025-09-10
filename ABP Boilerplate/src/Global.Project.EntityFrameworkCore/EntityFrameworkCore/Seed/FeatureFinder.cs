using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Localization;
using Abp.UI.Inputs;

namespace Global.Project.EntityFrameworkCore.Seed
{

    public static class FeatureFinder
    {
        public static List<Feature> GetAllFeatures(FeatureProvider provider)
        {
            var context = new CustomFeatureDefinitionContext();
            provider.SetFeatures(context); // This will populate context.Features
            return context.Features.ToList();
        }

        private class CustomFeatureDefinitionContext : IFeatureDefinitionContext
        {
            private readonly List<Feature> _features = new List<Feature>();

            public Feature Create(
                string name,
                string defaultValue,
                ILocalizableString displayName = null,
                ILocalizableString description = null,
                FeatureScopes scope = FeatureScopes.All,
                IInputType inputType = null)
            {
                var feature = new Feature(name, defaultValue, displayName, description, scope, inputType);
                _features.Add(feature);
                return feature;
            }

            public Feature GetOrNull(string name)
            {
                return _features.FirstOrDefault(f => f.Name == name);
            }

            public void Remove(string name)
            {
                var feature = _features.FirstOrDefault(f => f.Name == name);
                if (feature != null)
                {
                    _features.Remove(feature);
                }
            }

            public IReadOnlyList<Feature> Features => _features;
        }
    }
}
