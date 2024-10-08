//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v14.2.0+1b21caa
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	/// <summary>Section</summary>
	[PublishedModel("section")]
	public partial class Section : PublishedElementModel
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		public new const string ModelTypeAlias = "section";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<Section, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public Section(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Add Negative Spacing Bottom
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("addNegativeSpacingBottom")]
		public virtual bool AddNegativeSpacingBottom => this.Value<bool>(_publishedValueFallback, "addNegativeSpacingBottom");

		///<summary>
		/// Add Space To Bottom
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("addSpaceToBottom")]
		public virtual bool AddSpaceToBottom => this.Value<bool>(_publishedValueFallback, "addSpaceToBottom");

		///<summary>
		/// Add Space To Top
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("addSpaceToTop")]
		public virtual bool AddSpaceToTop => this.Value<bool>(_publishedValueFallback, "addSpaceToTop");

		///<summary>
		/// Hide Background In Mobile Design
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("hideBackgroundInMobileDesign")]
		public virtual bool HideBackgroundInMobileDesign => this.Value<bool>(_publishedValueFallback, "hideBackgroundInMobileDesign");

		///<summary>
		/// Hide Section
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("hideSection")]
		public virtual bool HideSection => this.Value<bool>(_publishedValueFallback, "hideSection");

		///<summary>
		/// Limit Dynamic Container Width In Mobile: Sets the max-width for dynamic container to 50%. Works up to 1023px wide
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("limitDynamicContainerWidthInMobile")]
		public virtual bool LimitDynamicContainerWidthInMobile => this.Value<bool>(_publishedValueFallback, "limitDynamicContainerWidthInMobile");

		///<summary>
		/// Limit Dynamic Container Width In Tablet: Sets the max-width for dynamic container to 50%. Works between 768 -1023px wide
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("limitDynamicContainerWidthInTablet")]
		public virtual bool LimitDynamicContainerWidthInTablet => this.Value<bool>(_publishedValueFallback, "limitDynamicContainerWidthInTablet");

		///<summary>
		/// Section Background Color
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sectionBackgroundColor")]
		public virtual global::Umbraco.Cms.Core.PropertyEditors.ValueConverters.ColorPickerValueConverter.PickedColor SectionBackgroundColor => this.Value<global::Umbraco.Cms.Core.PropertyEditors.ValueConverters.ColorPickerValueConverter.PickedColor>(_publishedValueFallback, "sectionBackgroundColor");

		///<summary>
		/// Section Background Height: If you have the height of the background image then set it here for better scaling.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("sectionBackgroundHeight")]
		public virtual int SectionBackgroundHeight => this.Value<int>(_publishedValueFallback, "sectionBackgroundHeight");

		///<summary>
		/// Section Background Image: Will  add a backgroudn with position absolute achored to top.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sectionBackgroundImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops SectionBackgroundImage => this.Value<global::Umbraco.Cms.Core.Models.MediaWithCrops>(_publishedValueFallback, "sectionBackgroundImage");

		///<summary>
		/// Section Title
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("sectionTitle")]
		public virtual string SectionTitle => this.Value<string>(_publishedValueFallback, "sectionTitle");

		///<summary>
		/// Set Background Color Absolute
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setBackgroundColorAbsolute")]
		public virtual bool SetBackgroundColorAbsolute => this.Value<bool>(_publishedValueFallback, "setBackgroundColorAbsolute");

		///<summary>
		/// Set Img Bottom: Will anchor image bottom
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setImgBottom")]
		public virtual bool SetImgBottom => this.Value<bool>(_publishedValueFallback, "setImgBottom");

		///<summary>
		/// Set Img Left: Will anchor image left. Set Img Right must be unchecked.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setImgLeft")]
		public virtual bool SetImgLeft => this.Value<bool>(_publishedValueFallback, "setImgLeft");

		///<summary>
		/// Set Img Right: Will anchor image right. Set Img Left must be unchecked.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "14.2.0+1b21caa")]
		[ImplementPropertyType("setImgRight")]
		public virtual bool SetImgRight => this.Value<bool>(_publishedValueFallback, "setImgRight");
	}
}
